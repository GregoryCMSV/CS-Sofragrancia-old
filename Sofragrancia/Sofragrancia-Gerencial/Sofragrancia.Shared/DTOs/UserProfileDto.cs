namespace Sofragrancia.Shared.Dtos;

public class UserProfileDto
{
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PerfilAcesso { get; set; } = string.Empty;
    
    // Propriedades exclusivas para o fluxo de alteração de senha
    public string SenhaAntiga { get; set; } = string.Empty;
    public string NovaSenha { get; set; } = string.Empty;
    public string ConfirmacaoNovaSenha { get; set; } = string.Empty;
}