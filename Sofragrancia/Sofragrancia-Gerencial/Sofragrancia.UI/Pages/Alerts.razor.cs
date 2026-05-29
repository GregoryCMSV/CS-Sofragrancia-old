using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Sofragrancia.UI.Components;

namespace Sofragrancia.UI.Pages;

public partial class Alerts : ComponentBase 
{
    // Controla a alternância fluida de abas (Continua igual)
    protected string abaAtiva = "configurar";

    // O objeto "ConfiguracaoAlerta" antigo foi REMOVIDO daqui, 
    // já que as abas agora gerenciam seus próprios DTOs isoladamente.

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