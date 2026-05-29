namespace Sofragrancia.Shared.Dtos;

public class AlertConfigurationItemDto
{
    public required string Id { get; set; }
    public required string Title { get; set; }
    public required string Operator { get; set; }
    public required string Unit { get; set; }
    public double Value { get; set; }
    public bool IsActive { get; set; }
}