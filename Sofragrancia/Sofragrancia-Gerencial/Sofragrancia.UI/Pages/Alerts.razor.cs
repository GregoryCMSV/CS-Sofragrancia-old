using Microsoft.AspNetCore.Components;
using Sofragrancia.UI.Components;
using Sofragrancia.UI.Components.Alerts;

namespace Sofragrancia.UI.Pages 
{
    public partial class Alerts : ComponentBase 
    {
        // Controla a alternância fluida de abas
        protected string abaAtiva = "configurar";

        // Objeto que será preenchido pelos inputs e persistido no Supabase futuramente
        protected ConfiguracaoAlerta Config { get; set; } = new ConfiguracaoAlerta();

        protected List<TabNavigation.TabItem> tabs = new()
        {
            new()
            {
                Id = "configurar",
                Title = "Configurar Alertas",
                Icon = "⚙️"
            },

            new()
            {
                Id = "relatorio",
                Title = "Relatórios",
                Icon = "📊"
            }
        };

        protected void AlternarAba(string novaAba)
        {
            abaAtiva = novaAba;
        }
    }
}