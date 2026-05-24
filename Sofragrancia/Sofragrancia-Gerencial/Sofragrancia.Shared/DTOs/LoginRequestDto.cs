using System.ComponentModel.DataAnnotations;

namespace Sofragrancia.Shared.DTOs;

public class LoginRequestDto
{
    [Required(ErrorMessage = "O email é obrigatorio")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "A senha é obrigatoria")]
    public string Password { get; set; } = string.Empty;
}