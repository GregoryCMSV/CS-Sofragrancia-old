namespace Sofragrancia.Shared.Dtos;

public class UserRegistrationDto
{
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PerfilAcesso { get; set; } = "Vendedor";
    public string Senha { get; set; } = string.Empty;
}