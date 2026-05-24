using Microsoft.AspNetCore.Components;
namespace Sofragrancia.UI.Components.Alerts;

public partial class MonthlyReport
{ 
    [Parameter]
    public ConfiguracaoAlerta Config { get; set; } = new ();

    protected void EnviarTesteRelatorio()
    {
        System.Diagnostics.Debug.WriteLine($"[Relatório] Enviando e-mail de teste para: {Config.EmailDestinatario}");
    }

    
    protected List<RelatorioSecaoItem> SecoesRelatorio = new()
    {
        new() { Id = "dashboard", Titulo = "Dashboard Geral", Icone = "📊" },
        new() { Id = "cotas", Titulo = "Cotas e Atingimento", Icone = "🎯" },
        new() { Id = "faturamento", Titulo = "Faturamento Detalhado", Icone = "💰" },
        new() { Id = "ranking", Titulo = "Ranking Vendedores", Icone = "🏆" },
        new() { Id = "produtos", Titulo = "Performance Produtos", Icone = "📦" },
        new() { Id = "clientes", Titulo = "Análise de Clientes", Icone = "👥" },
        new() { Id = "cancelamentos", Titulo = "Cancelamentos", Icone = "❌" },
        new() { Id = "estoque", Titulo = "Posição de Estoque", Icone = "🏭" },
        new() { Id = "alertas", Titulo = "Alertas Disparados", Icone = "🛡️" },
        new() { Id = "precos", Titulo = "Variação de Preços", Icone = "💹" },
        new() { Id = "bi", Titulo = "15 Análises de BI", Icone = "📈" },
        new() { Id = "projecao", Titulo = "Projeções Próx. Mês", Icone = "🔮" }
    };
}