using Sofragrancia.Banco.Models;
using Sofragrancia.Banco.Models.Alertas;
using Sofragrancia_EmailSender.Interfaces;
using Supabase;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sofragrancia_EmailSender.Strategies
{
    public class EstoqueMaximoStrategy : IAlertStrategy
    {
        public async Task<string> GenerateHtmlAlertAsync(Client client, AlertaConfigUser config)
        {
            var produtos = await client.From<Produto>().Get();
            var produtosAtivos = produtos.Models.Where(p => p.IsEnable);

            Func<Produto, bool> filtro = (config.UnidadeMedida, config.Trigger) switch
            {
                (1, 4) => p => Convert.ToDouble(p.EstoqueAtual) > config.Value,
                (1, 5) => p => Convert.ToDouble(p.EstoqueAtual) >= config.Value,
                (2, 4) => p => Convert.ToDouble(p.EstoqueAtual) > ((config.Value/100) * Convert.ToDouble(p.EstoqueMinimo)),
                (2, 5) => p => Convert.ToDouble(p.EstoqueAtual) >= ((config.Value) * Convert.ToDouble(p.EstoqueMinimo)),
                _ => p => false
            };

            List<Produto> produtosAcima = produtosAtivos.Where(filtro).ToList();

            if (!produtosAcima.Any())
                return string.Empty;

            return GenerateString(produtosAcima, config);
        }

        private string GenerateString(List<Produto> produtos, AlertaConfigUser config)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<div class='alert-box warning'>");
            sb.AppendLine("<h3 class='alert-title danger'>Produtos Atingiram Estoque Máximo</h3>");
            sb.AppendLine("<ul class='alert-list'>");
            foreach (var prod in produtos)
            {
                sb.AppendLine($"<li><b>{prod.Descricao}</b> - Atual: {prod.EstoqueAtual}  (Máximo: {(config.UnidadeMedida == 1? config.Value : (config.Value * Convert.ToDouble(prod.EstoqueMinimo)).ToString("F2"))})</li>");
            }
            sb.AppendLine("</ul></div>");

            return sb.ToString();
        }


    }
}
