using System.ComponentModel.DataAnnotations;

namespace Sofragrancia.Shared.Dtos;

public class RecuperarSenhaRequestDto
{
    [Required(ErrorMessage = "O e-mail é obrigatório.")]
    [EmailAddress(ErrorMessage = "Por favor, insira um endereço de e-mail válido.")]
    public string Email { get; set; } = string.Empty;
}