using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Sofragrancia.Banco;

namespace Sofragrancia.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        AuthService _authService;

        public AuthController(Supabase.Client client)
        {
            _authService = new(client);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {               
                var session = await _authService.LoginAsync(request.Email, request.Password);

                return Ok(new
                {
                    Mensagem = "Login realizado com sucesso",
                    Token = session.AccessToken
                });
            }
            catch (Exception ex)
            {
                return Unauthorized(new { Erro = "Credenciais inválidas ou usuário não encontrado." });
            }
        }
    }

    public class LoginRequest
    {
        [Required(ErrorMessage = "O email é obrigatorio")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "A senha é obrigatoria")]
        public string Password { get; set; } = string.Empty;
    }
}

