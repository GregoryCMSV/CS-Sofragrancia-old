using Microsoft.AspNetCore.Mvc;
using Sofragrancia.API.Services;
using Sofragrancia.Banco;
using Sofragrancia.Banco.Repositories;
using Sofragrancia.Shared.Dtos;
using Supabase.Gotrue;
using System.IdentityModel.Tokens.Jwt;

namespace Sofragrancia.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        AuthService _authService;
        AlertService _alertController;
        RabbitMqService _rabbitMqService;
        
        IConfiguration _configuration;


        public AuthController(Supabase.Client client, AlertService alertController, IConfiguration configuration, RabbitMqService rabbitMqService)
        {
            _authService = new(client);
            _alertController = alertController;
            _configuration = configuration;
            _rabbitMqService = rabbitMqService;
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

        [HttpPost("cadastro")]
        public async Task<IActionResult> Cadastrar([FromBody] NewUserRequestDto request)
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString();
                if (!_authService.ValidarMetadadosDoToken(token, "Admin"))
                {
                    return StatusCode(403, new { Erro = "Acesso negado. Apenas administradores podem cadastrar." });
                }

                (string novoUserId, string errorMessage) = await _authService.CadastrarUsuarioInternoAsync(
                    request,
                    _configuration["Supabase:Pkey"]!
                    );

                if(string.IsNullOrEmpty(novoUserId)) return BadRequest(new { Erro = errorMessage });

                await _alertController.SincronizarAlertasDoUsuarioAsync(novoUserId, request.Email);

                return Ok(new { Mensagem = "Usuário e alertas configurados com sucesso!", UserId = novoUserId });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Erro = "Falha ao processar o cadastro", Detalhe = ex.Message });
            }
        }

        [HttpGet("validate")]
        public async Task<IActionResult> ValidateToken()
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString();
                if (!_authService.ValidarMetadadosDoToken(token))
                {
                    return Ok(new { IsValid = false,Message = "Token invalido" });
                }               
                return Ok(new { IsValid = true, Message = "Token Valido" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Erro = "Falha ao validar o token", ex.Message });
            }
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] PasswordUpdateRequest request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Senha))
                    return BadRequest(new { Message = "Informe o usuário e a nova senha" });

                var response = await _authService.ReiniciarSenhaUsuarioAsync(request.Email, request.Senha, _configuration["Supabase:Pkey"]!);

                if (response == null)
                    return BadRequest(new { Message = "Erro ao atualizar a senha" });

                await _rabbitMqService.PublicarEmailTrocaSenha(request.Email, request.Senha);
                return Ok("Senha atualizada, verifique o email cadastrado");
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }



    }
}

