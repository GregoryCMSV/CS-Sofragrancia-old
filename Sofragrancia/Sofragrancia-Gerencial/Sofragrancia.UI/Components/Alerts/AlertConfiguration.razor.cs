using Microsoft.AspNetCore.Components;
namespace Sofragrancia.UI.Components.Alerts;

public partial class AlertConfiguration
{ 
    [Parameter]
    public ConfiguracaoAlerta Config { get; set; } = new ();

    protected void SalvarConfiguracoes()
    {   
        System.Diagnostics.Debug.WriteLine($"[Config] Alertas salvos para o e-mail: {Config.EmailDestinatario}");
    }

    protected List<AlertConfigItem> AlertConfigs = new()
    {
        new()
        {
            Id = "estoque_critico",
            Title = "🏭 Estoque Crítico",
            Operator = "≤",
            Unit = "un.",
            Value = 50.0,
            IsActive = true
        },

        new()
        {
            Id = "cancelamento",
            Title = "❌ Taxa de Cancelamento",
            Operator = "≤",
            Unit = "%",
            Value = 5.0,
            IsActive = true
        },

        new()
        {
            Id = "cobertura_cota",
            Title = "🎯 Cobertura de Cota",
            Operator = "≤",
            Unit = "%",
            Value = 40.0,
            IsActive = true
        },

        new()
        {
            Id = "desconto_excessivo",
            Title = "💲 Desconto Excessivo",
            Operator = "≥",
            Unit = "%",
            Value = 7.0,
            IsActive = true
        },

        new()
        {
            Id = "meta_risco",
            Title = "📈 Meta Mensal em Risco",
            Operator = "≤",
            Unit = "% meta",
            Value = 90.0,
            IsActive = true
        },

        new()
        {
            Id = "cliente_inativo",
            Title = "🔄 Cliente Inativo",
            Operator = "≥",
            Unit = "dias",
            Value = 20.0,
            IsActive = true
        },

        new()
        {
            Id = "preco_defasado",
            Title = "💹 Variação de Preço Defasada",
            Operator = "≥",
            Unit = "meses",
            Value = 3.0,
            IsActive = false
        }
    };
}