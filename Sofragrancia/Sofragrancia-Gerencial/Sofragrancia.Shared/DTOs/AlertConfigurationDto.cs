namespace Sofragrancia.Shared.Dtos;

public class AlertConfigurationDto
{
    // --- Canais de Notificação e Frequência ---
    public string EmailDestinatario { get; set; } = string.Empty;
    public bool EmailAtivo { get; set; }
    public TimeOnly? HorarioEnvio { get; set; }
    
    // Dias da semana
    public bool FaturamentoSegunda { get; set; }
    public bool FaturamentoTerca { get; set; }
    public bool FaturamentoQuarta { get; set; }
    public bool FaturamentoQuinta { get; set; }
    public bool FaturamentoSexta { get; set; }
    public bool FaturamentoSabado { get; set; }
    public bool FaturamentoDomingo { get; set; }

    // --- Lista de Indicadores da Tabela ---
    public List<AlertConfigurationItemDto> Indicators { get; set; } = new();
}