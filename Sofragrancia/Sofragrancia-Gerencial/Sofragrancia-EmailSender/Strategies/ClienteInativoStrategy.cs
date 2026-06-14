using Sofragrancia.Banco.Models;
using Sofragrancia.Banco.Models.Alertas;
using Sofragrancia_EmailSender.Interfaces;
using Supabase;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sofragrancia_EmailSender.Strategies
{
    public class ClienteInativoStrategy : IAlertStrategy
    {
        public async Task<string> GenerateHtmlAlertAsync(Client client, AlertaConfigUser config)
        {
            var pedidosReq = await client.From<Pedido>().Get();
            var clientesReq = await client.From<Cliente>().Get();

            var pedidosAtivos = pedidosReq.Models
                .Where(p => !p.Status.Equals("cancelado", StringComparison.OrdinalIgnoreCase))
                .ToList();

            var clientesAtivos = clientesReq.Models
                .Where(c => c.IsEnable)
                .ToList();

            var clientesParaAlertar = new List<(Cliente Cliente, int DiasInativos, DateTime? UltimaCompra)>();

            DateTime dataCorte = config.UnidadeMedida == 4
                ? DateTime.Today.AddMonths(-(int)config.Value)
                : DateTime.Today.AddDays(-(int)config.Value);

            foreach (var cliente in clientesAtivos)
            {
                var pedidosDoCliente = pedidosAtivos.Where(p => p.IdCliente == cliente.Id).ToList();

                if (pedidosDoCliente.Any())
                {
                    var ultimaCompra = pedidosDoCliente.Max(p => p.DocDate);
                    var diasInativos = (DateTime.Today - ultimaCompra.Date).Days;

                    bool atendeRegra = config.Trigger switch
                    {
                        4 => ultimaCompra.Date <= dataCorte,
                        5 => ultimaCompra.Date < dataCorte,
                        _ => false 
                    };

                    if (atendeRegra)
                    {
                        clientesParaAlertar.Add((cliente, diasInativos, ultimaCompra));
                    }
                }
                else
                {
                    clientesParaAlertar.Add((cliente, -1, null));
                }
            }

            if (!clientesParaAlertar.Any())
                return string.Empty;

            return GenerateString(clientesParaAlertar, config);
        }

        private string GenerateString(List<(Cliente Cliente, int DiasInativos, DateTime? UltimaCompra)> inativos, AlertaConfigUser config)
        {
            string unidadeTexto = config.UnidadeMedida == 4 ? "meses" : "dias";
            string condicaoTexto = config.Trigger == 4 ? "Mais de ou igual a" : "Mais de";
            string subtitulo = $"{condicaoTexto} {config.Value} {unidadeTexto}";

            var sb = new StringBuilder();
            sb.AppendLine("<div class='alert-box warning'>");
            sb.AppendLine($"<h3 class='alert-title warning'> Clientes Sem Compra Recente</h3>");
            sb.AppendLine($"<p class='alert-subtitle warning'>Regra: {subtitulo} sem comprar</p>");
            sb.AppendLine("<ul class='alert-list'>");

            foreach (var item in inativos.OrderByDescending(i => i.DiasInativos))
            {
                if (item.UltimaCompra.HasValue)
                {
                    sb.AppendLine($"<li><b>{item.Cliente.Name}</b> - Última compra: {item.UltimaCompra.Value:dd/MM/yyyy} (Há <b>{item.DiasInativos}</b> dias)</li>");
                }
                else
                {
                    sb.AppendLine($"<li><b>{item.Cliente.Name}</b> - <i>Nunca realizou uma compra.</i></li>");
                }
            }

            sb.AppendLine("</ul></div>");

            return sb.ToString();
        }
    }
}
