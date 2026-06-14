namespace Sofragrancia.Shared.Dtos;

// DTO que espelha exatamente o JSON retornado pelo GET /api/Alerta/email/{email}
// Não misture com AlertConfigurationDto — esse é só para desserializar a resposta do banco.

public class AlertaHeaderResponseDto
{
    public int? Id { get; set; }
    public string? IdUsuario { get; set; } = string.Empty;
    public string? Email { get; set; } = string.Empty;
    public string? Horario { get; set; } = "08:00:00"; // vem como string "HH:mm:ss"
    public int[]? Dias { get; set; } = Array.Empty<int>();
    public bool? IsEnable { get; set; }
    public List<AlertaConfigUserResponseDto>? Alertas { get; set; } = new();
}

public class AlertaConfigUserResponseDto
{
    public int? Id { get; set; }
    public int? IdHeader { get; set; }
    public int? IdAlertaBase { get; set; }
    public AlertaBaseResponseDto? AlertaBaseDados { get; set; } = new();
    public int? Trigger { get; set; }
    public int? UnidadeMedida { get; set; }
    public double? Value { get; set; }
    public bool? IsEnable { get; set; }
}

public class AlertaBaseResponseDto
{
    public int? Id { get; set; }
    public string? Name { get; set; } = string.Empty;
    public string? Descricao { get; set; }
    public int? Trigger { get; set; }
    public int[]? ValidTrigger { get; set; } = Array.Empty<int>();
    public int? UnidadeMedida { get; set; }
    public int[]? UnidadeMedidaValidas { get; set; } = Array.Empty<int>();
    public double? Value { get; set; }
    public bool? IsEnable { get; set; }
}