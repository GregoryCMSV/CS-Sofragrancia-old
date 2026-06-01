namespace Sofragrancia.Shared.Dtos;

public class LoginResponseDto
{
    public string Mensagem { get; set; } = string.Empty;
    public string? Token { get; set; }
}