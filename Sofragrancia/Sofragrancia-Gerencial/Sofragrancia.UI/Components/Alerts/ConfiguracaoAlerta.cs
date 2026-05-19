 namespace Sofragrancia.UI.Components.Alerts;
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
    }