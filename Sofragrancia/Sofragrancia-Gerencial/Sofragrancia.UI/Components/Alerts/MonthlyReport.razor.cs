using Microsoft.AspNetCore.Components;
namespace Sofragrancia.UI.Components.Alerts;

public partial class MonthlyReport
{ 
    [Parameter]
    public ConfiguracaoAlerta Config { get; set; } = new ();

    protected void VisualizarPreviewRelatorio()
    {
        System.Diagnostics.Debug.WriteLine("[Relatório] Gerando visualização prévia...");
    }

    protected void EnviarTesteRelatorio()
    {
        System.Diagnostics.Debug.WriteLine($"[Relatório] Enviando e-mail de teste para: {Config.EmailDestinatario}");
    }
}