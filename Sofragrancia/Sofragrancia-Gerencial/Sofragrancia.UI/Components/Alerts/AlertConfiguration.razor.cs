using Microsoft.AspNetCore.Components;
namespace Sofragrancia.UI.Components.Alerts;

public partial class AlertConfiguration
{ 
    [Parameter]
    public ConfiguracaoAlerta Config { get; set; } = new ();

    protected void SalvarConfiguracoes()
    {
        // O código que se comunicará com o banco de dados entrará aqui
        // Exemplo: await SupabaseClient.From<ConfiguracaoAlerta>().Insert(Config);
        
        System.Diagnostics.Debug.WriteLine($"[Config] Alertas salvos para o e-mail: {Config.EmailDestinatario}");
    }
    
}