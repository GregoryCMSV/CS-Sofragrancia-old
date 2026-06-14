using Sofragrancia.Banco.Models;
using Sofragrancia.Banco.Models.Alertas;
using Sofragrancia_EmailSender.Interfaces;
using Supabase;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sofragrancia_EmailSender.Strategies
{
    public class TaxaCancelamentoStrategy : IAlertStrategy
    {
        public async Task<string> GenerateHtmlAlertAsync(Client client, AlertaConfigUser config)
        {
            var pedidos = await client.From<Pedido>().Get();
            var pedidosMes = pedidos.Models.Where(p => p.DocDate.Month == DateTime.Today.Month).ToList();

            if (!pedidosMes.Any())
                return  @"
                    <div class='alert-box info'>
                        <h3 class='alert-title info'>Taxa de Cancelamento</h3>
                        <ul class='alert-list'>
                            <li><b>Ainda não houve pedidos este mês.</b></li>
                        </ul>
                    </div>";

            var totalPedidos = pedidosMes.Count();
            var pedidosCancelados = pedidosMes.Where(p => p.Status.Equals("cancelado", StringComparison.OrdinalIgnoreCase)).Count();

            return GenerateString(totalPedidos, pedidosCancelados, config);
        }

        private string GenerateString(int totalPedidos,int pedidosCancelados, AlertaConfigUser config)
        {
            double porcentagem = totalPedidos > 0 ? ((double)pedidosCancelados / totalPedidos) * 100 : 0;
            bool limiteUltrapassado = config.Trigger switch
            {
                4 => porcentagem >= config.Value, 
                5 => porcentagem > config.Value,  
                _ => false
            };

            string boxClass = limiteUltrapassado ? "danger" : "warning"; 
            string cssStatusText = limiteUltrapassado ? "text-danger" : "text-success"; 
            string statusTexto = limiteUltrapassado ? "Acima do limite (Atenção)" : "Abaixo do limite (Seguro)";
            string condicaoRegra = config.Trigger == 4 ? "Maior ou igual a" : "Maior que";

            var sb = new StringBuilder();

            sb.AppendLine($"<div class='alert-box {boxClass}'>");
            sb.AppendLine($"<h3 class='alert-title {boxClass}'>Taxa de Cancelamento</h3>");
            sb.AppendLine($"<p class='alert-subtitle {boxClass}'>Regra do alerta: {condicaoRegra} {config.Value}%</p>");

            sb.AppendLine("<ul class='alert-list unstyled'>");

            sb.AppendLine($"<li class='{cssStatusText}' style='font-size: 16px; margin-bottom: 8px;'>");
            sb.AppendLine($"Status: {statusTexto} - {porcentagem:F1}%");
            sb.AppendLine("</li>");

            sb.AppendLine("<li>");
            sb.AppendLine($"Detalhes: Foram cancelados <b>{pedidosCancelados}</b> pedidos de um total de <b>{totalPedidos}</b> neste mês.");
            sb.AppendLine("</li>");

            sb.AppendLine("</ul></div>");

            return sb.ToString();
        }

        
    }
}
