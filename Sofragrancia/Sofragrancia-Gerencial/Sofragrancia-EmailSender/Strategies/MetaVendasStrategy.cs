using Sofragrancia.Banco.Models;
using Sofragrancia.Banco.Models.Alertas;
using Sofragrancia_EmailSender.Interfaces;
using Supabase;
using System;
using System.Collections.Generic;
using System.Text;
using static Supabase.Postgrest.Constants;

namespace Sofragrancia_EmailSender.Strategies
{
    public class MetaVendasStrategy : IAlertStrategy
    {
        public async Task<string> GenerateHtmlAlertAsync(Client client, AlertaConfigUser config)
        {
            int anoAtual = DateTime.Today.Year;
            int mesAtual = DateTime.Today.Month;

            var metasReq = await client.From<MetaVendas>().Get();
            var metasDoMes = metasReq.Models
                .Where(m => m.Ano == anoAtual && m.Mes == mesAtual && m.IsEnable)
                .ToList();

            if (!metasDoMes.Any())
            {
                return @"
                        <div class='alert-box info'>
                        <h3 class='alert-title info'>Metas de Vendas</h3>
                        <p class='alert-text info'>As metas de vendas para este mês ainda não foram definidas</p>
                        </div>"; 
            }

            var vendedoresReq = await client.From<Vendedor>().Get();
            var produtosReq = await client.From<Produto>().Get();

            var vendedoresDict = vendedoresReq.Models.ToDictionary(v => v.Id, v => v.Name);
            var produtosDict = produtosReq.Models.ToDictionary(p => p.Id, p => p.Descricao);

            var vendedoresAbaixoDaMeta = new List<(string NomeVendedor, List<string> MetasHtml)>();

            var metasPorVendedor = metasDoMes.GroupBy(m => m.IdVendedor);

            foreach (var grupo in metasPorVendedor)
            {
                int idVendedor = grupo.Key;
                string nomeVendedor = vendedoresDict.TryGetValue(idVendedor, out var nome) ? nome : $"Vendedor {idVendedor}";
                var metasFalhasHtml = new List<string>();

                foreach (var meta in grupo)
                {
                    if (Convert.ToDouble(meta.ValorMeta) <= 0) continue;

                    double valorMeta = Convert.ToDouble(meta.ValorMeta);
                    double valorRealizado = Convert.ToDouble(meta.ValorRealizado);

                    double porcentagemAtingida = (valorRealizado / valorMeta) * 100;

                    bool abaixoDaRegra = config.Trigger switch
                    {
                        1 => porcentagemAtingida < config.Value,
                        2 => porcentagemAtingida <= config.Value,
                        _ => false
                    };

                    if (abaixoDaRegra)
                    {
                        string nomeProduto = produtosDict.TryGetValue(meta.IdProduto, out var prod) ? prod : $"Produto {meta.IdProduto}";
                        string itemHtml = $"<li><b>{nomeProduto}</b>: Atingiu apenas <span style='color: #d9534f;'>{porcentagemAtingida:F1}%</span> " +
                                          $"(R$ {valorRealizado:N2} de R$ {valorMeta:N2} | " +
                                          $"Qtd: {meta.QtdRealizada} de {meta.QtdDemanda})</li>";

                        metasFalhasHtml.Add(itemHtml);
                    }
                }
                if (metasFalhasHtml.Any())
                {
                    vendedoresAbaixoDaMeta.Add((nomeVendedor, metasFalhasHtml));
                }
            }

            if (!vendedoresAbaixoDaMeta.Any())
                return string.Empty;

            return GenerateHtmlString(vendedoresAbaixoDaMeta, config);
        }

        private string GenerateHtmlString(List<(string NomeVendedor, List<string> MetasHtml)> dados, AlertaConfigUser config)
        {
            var sb = new StringBuilder();

            string condicaoTexto = config.Trigger == 1 ? "Abaixo de" : "Abaixo ou igual a";

            sb.AppendLine("<div class='alert-box danger'>");
            sb.AppendLine($"<h3 class='alert-title danger'> Vendedores Abaixo da Meta</h3>");
            sb.AppendLine($"<p class='alert-subtitle danger'>Aviso para atingimento {condicaoTexto} {config.Value}%</p>");

            foreach (var vendedor in dados)
            {
                sb.AppendLine($"<h4 style='color: #a94442; margin-bottom: 5px; margin-top: 15px;'>{vendedor.NomeVendedor}</h4>");
                sb.AppendLine("<ul style='color: #333; margin-top: 0;'>");

                foreach (var metaHtml in vendedor.MetasHtml)
                {
                    sb.AppendLine(metaHtml);
                }

                sb.AppendLine("</ul>");
            }

            sb.AppendLine("</div>");

            return sb.ToString();
        }
    }
}
