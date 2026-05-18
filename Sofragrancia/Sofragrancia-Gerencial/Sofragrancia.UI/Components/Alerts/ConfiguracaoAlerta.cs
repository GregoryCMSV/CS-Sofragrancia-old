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