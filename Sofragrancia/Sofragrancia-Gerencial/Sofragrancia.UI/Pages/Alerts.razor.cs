using Microsoft.AspNetCore.Components;

namespace Sofragrancia.UI.Pages
{
    public partial class Alerts
    {
        private List<AlertaModel> ListaAlertas { get; set; } = new();

        protected override void OnInitialized()
        {
            // Simulando alertas do sistema
            ListaAlertas = new List<AlertaModel>
            {
                new AlertaModel { 
                    Data = DateTime.Now.AddMinutes(-45), 
                    Categoria = "Estoque", 
                    Mensagem = "Produto 'SOXERO Gold' atingiu o nível crítico (5 unidades).", 
                    CorClasse = "bg-warning text-dark",
                    Lido = false 
                },
                new AlertaModel { 
                    Data = DateTime.Now.AddHours(-2), 
                    Categoria = "Vendas", 
                    Mensagem = "Venda atípica detectada: R$ 15.400,00 - Vendedor: João Silva.", 
                    CorClasse = "bg-info text-white",
                    Lido = false 
                },
                new AlertaModel { 
                    Data = DateTime.Today.AddHours(-5), 
                    Categoria = "Financeiro", 
                    Mensagem = "3 faturas de clientes VIP vencem amanhã.", 
                    CorClasse = "bg-danger text-white",
                    Lido = false 
                },
                new AlertaModel { 
                    Data = DateTime.Today.AddDays(-1), 
                    Categoria = "Sistema", 
                    Mensagem = "Integração com e-commerce finalizada com sucesso.", 
                    CorClasse = "bg-success text-white",
                    Lido = true 
                }
            };
        }

        private void MarcarComoLido(AlertaModel alerta)
        {
            alerta.Lido = true;
            StateHasChanged(); // Força a tela a atualizar
        }

        public class AlertaModel
        {
            public DateTime Data { get; set; }
            public string Categoria { get; set; } = "";
            public string Mensagem { get; set; } = "";
            public string CorClasse { get; set; } = "";
            public bool Lido { get; set; }
        }
    }
}