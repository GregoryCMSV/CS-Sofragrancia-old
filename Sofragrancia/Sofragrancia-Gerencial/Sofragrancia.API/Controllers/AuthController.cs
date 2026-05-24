using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Sofragrancia.Banco;
using Sofragrancia.Shared.DTOs;

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
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            try
            {               
                var session = await _authService.LoginAsync(request.Email, request.Password);

                return Ok(new LoginResponseDto
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
}

