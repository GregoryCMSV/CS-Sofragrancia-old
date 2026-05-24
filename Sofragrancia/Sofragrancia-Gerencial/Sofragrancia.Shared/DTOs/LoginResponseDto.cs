namespace Sofragrancia.Shared.DTOs;

public class LoginResponseDto
{
    public string Mensagem { get; set; } = string.Empty;
    public string? Token { get; set; }
}