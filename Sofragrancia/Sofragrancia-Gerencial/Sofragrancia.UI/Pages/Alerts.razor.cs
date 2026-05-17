using Microsoft.AspNetCore.Components;

namespace Sofragrancia.UI.Pages 
{
    public partial class Alerts : ComponentBase 
    {
        // Controla a alternância fluida de abas
        protected string abaAtiva = "configurar";

        // Objeto que será preenchido pelos inputs e persistido no Supabase futuramente
        protected ConfiguracaoAlerta Config { get; set; } = new ConfiguracaoAlerta();

        protected void AlternarAba(string novaAba)
        {
            abaAtiva = novaAba;
            StateHasChanged();
        }

        protected void SalvarConfiguracoes()
        {
            // O código que se comunicará com o banco de dados entrará aqui
            // Exemplo: await SupabaseClient.From<ConfiguracaoAlerta>().Insert(Config);
            
            System.Diagnostics.Debug.WriteLine($"[Config] Alertas salvos para o e-mail: {Config.EmailDestinatario}");
        }

        protected void VisualizarPreviewRelatorio()
        {
            System.Diagnostics.Debug.WriteLine("[Relatório] Gerando visualização prévia...");
        }

        protected void EnviarTesteRelatorio()
        {
            System.Diagnostics.Debug.WriteLine($"[Relatório] Enviando e-mail de teste para: {Config.EmailDestinatario}");
        }
    }

    /// <summary>
    /// Modelo de dados estruturado para capturar os estados dos inputs na tela de Alertas.
    /// </summary>
    public class ConfiguracaoAlerta
    {
        // Canais de Notificação
        public bool EmailAtivo { get; set; } = true;
        public string EmailDestinatario { get; set; } = "gerente@sofragrancia.com.br";
        public TimeOnly? HorarioEnvio { get; set; } = new TimeOnly(7, 0);

        // Agendamento do Envio (Relatório Mensal)
        public string AgendamentoRelatorio { get; set; } = "UltimoDiaUtil";
        
        // Propriedades booleanas individuais para os dias da semana (Mapeadas direto para a tela)
        public bool FaturamentoSegunda { get; set; } = true;
        public bool FaturamentoTerca { get; set; } = true;
        public bool FaturamentoQuarta { get; set; } = true;
        public bool FaturamentoQuinta { get; set; } = true;
        public bool FaturamentoSexta { get; set; } = true;
        public bool FaturamentoSabado { get; set; } = false;
        public bool FaturamentoDomingo { get; set; } = false;
                
        // Status dos Gatilhos (Ativo/Inativo)
        public bool AlertaEstoqueAtivo { get; set; } = true;
        public bool AlertaCancelamentoAtivo { get; set; } = true;
        public bool AlertaCotaAtivo { get; set; } = true;
        public bool AlertaDescontoAtivo { get; set; } = true;
        public bool AlertaMetaAtivo { get; set; } = true;
        public bool AlertaInatividadeAtivo { get; set; } = true;
        public bool AlertaPrecoDefasado { get; set; } = false;

        // Valores numéricos limites
        public int LimiteEstoqueCritico { get; set; } = 50;
        public int LimiteCancelamento { get; set; } = 5;
        public int LimiteCoberturaCota { get; set; } = 40;
        public int LimiteDescontoExcessivo { get; set; } = 7;
        public int LimiteMetaRisco { get; set; } = 90;
        public int DiasInatividadeCliente { get; set; } = 20;
        public int MesesPrecoDefasado { get; set; } = 3;
    }
}