namespace Sofragrancia.Shared.Dtos;

public class MonthlyReportSectionDto
{
    public required string Id { get; set; }
    public required string Titulo { get; set; }
    public required string Icone { get; set; }
    public bool Ativo { get; set; } = true;
}