using Sofragrancia.Banco.Models;
using Sofragrancia.Banco.Models.Alertas;
using Sofragrancia_EmailSender.Interfaces;
using Supabase;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sofragrancia_EmailSender.Strategies
{
    public class EstoqueMinimoStrategy : IAlertStrategy
    {
        public async Task<string> GenerateHtmlAlertAsync(Client client, AlertaConfigUser config)
        {
            var produtos = await client.From<Produto>().Get();
            var produtosAtivos = produtos.Models.Where(p => p.IsEnable);

            Func<Produto, bool> filtro = (config.UnidadeMedida, config.Trigger) switch
            {
                (1, 1) => p => Convert.ToDouble(p.EstoqueAtual) <= config.Value,
                (1, 2) => p => Convert.ToDouble(p.EstoqueAtual) < config.Value,
                (2, 1) => p => Convert.ToDouble(p.EstoqueAtual) <= ((config.Value/100) * Convert.ToDouble(p.EstoqueMinimo)),
                (2, 2) => p => Convert.ToDouble(p.EstoqueAtual) < ((config.Value / 100) * Convert.ToDouble(p.EstoqueMinimo)),
                _ => p => false 
            };

            List<Produto> produtosAbaixo = produtosAtivos.Where(filtro).ToList();           

            if (!produtosAbaixo.Any())
                return string.Empty;           

            return GenerateString(produtosAbaixo);
        }


       private string GenerateString(List<Produto> produtos)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<div class='alert-box danger'>");
            sb.AppendLine("<h3 class='alert-title danger'>Produtos Atingiram Estoque Mínimo</h3>");
            sb.AppendLine("<ul class='alert-list'>");
            foreach (var prod in produtos)
            {
                sb.AppendLine($"<li><b>{prod.Descricao}</b> - Atual: {prod.EstoqueAtual} (Mínimo: {prod.EstoqueMinimo})</li>");
            }
            sb.AppendLine("</ul></div>");

            return sb.ToString();
        }
        
    }
}


