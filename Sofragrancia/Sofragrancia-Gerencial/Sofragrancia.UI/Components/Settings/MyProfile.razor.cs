using System;
using System.Threading.Tasks; 
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using Sofragrancia.Shared.Dtos;
using Sofragrancia.UI.Services; 
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace Sofragrancia.UI.Components.Settings;

public partial class MyProfile
{
    [Inject] protected HttpService HttpService { get; set; } = default!;
    
    // Injeta o TokenService para ler a sessão do usuário real
    [Inject] protected TokenService TokenService { get; set; } = default!;

    // Estado isolado usando o DTO da Shared que você já possui
    protected UserProfileDto Perfil { get; set; } = new();

    protected bool exibirMenuSenha = false;
    protected string MensagemSucessoPerfil { get; set; } = string.Empty;
    protected string MensagemErroPerfil { get; set; } = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        // 1. Busca os metadados (Nome e Cargo) usando o TokenService
        var dadosToken = await TokenService.ObterDadosUsuarioLogadoAsync();
        
        if (dadosToken != null)
        {
            Perfil.Nome = dadosToken.NomeCompleto;
            Perfil.PerfilAcesso = dadosToken.Role;
        }

        // 2. Busca o e-mail que fica no nó principal do Token JWT
        var tokenRaw = await TokenService.ObterTokenAsync();
        if (!string.IsNullOrWhiteSpace(tokenRaw))
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(tokenRaw.Replace("Bearer ", ""));
                
                // Extrai a claim padrão de e-mail do Supabase
                Perfil.Email = jwtToken.Claims.FirstOrDefault(c => c.Type == "email" || c.Type == "unique_name")?.Value ?? "usuario@sofragrancia.com.br";
            }
            catch (Exception)
            {
                Perfil.Email = "erro.usuario@sofragrancia.com.br";
            }
        }
    }

    protected void AlternarMenuSenha()
    {
        exibirMenuSenha = !exibirMenuSenha;
        
        if (!exibirMenuSenha)
        {
            Perfil.SenhaAntiga = string.Empty;
            Perfil.NovaSenha = string.Empty;
            Perfil.ConfirmacaoNovaSenha = string.Empty;
        }
    }

    protected async Task SalvarPerfil()
    {
        MensagemSucessoPerfil = string.Empty;
        MensagemErroPerfil = string.Empty;

        if (string.IsNullOrWhiteSpace(Perfil.SenhaAntiga) || 
            string.IsNullOrWhiteSpace(Perfil.NovaSenha) || 
            string.IsNullOrWhiteSpace(Perfil.ConfirmacaoNovaSenha))
        {
            MensagemErroPerfil = "Por favor, preencha todos os campos para efetuar a troca de senha.";
            return;
        }

        if (Perfil.NovaSenha.Length < 6)
        {
            MensagemErroPerfil = "A nova senha precisa ter no mínimo 6 caracteres.";
            return;
        }

        if (Perfil.NovaSenha != Perfil.ConfirmacaoNovaSenha)
        {
            MensagemErroPerfil = "A nova senha e a confirmação digitadas são diferentes.";
            return;
        }

        try
        {
            var requestBody = new UpdateUserRequestDto
            {
                Senha = Perfil.NovaSenha
            };

            var response = await HttpService.PatchAsync("api/auth/update-user", requestBody);

            if (!response.IsSuccessStatusCode)
            {
                MensagemErroPerfil = "Não foi possível atualizar a senha no servidor.";
                return;
            }

            MensagemSucessoPerfil = "Sua senha foi atualizada com sucesso!";
            AlternarMenuSenha();

        }
        catch (Exception)
        {
            MensagemErroPerfil = "Erro ao tentar atualizar sua senha de acesso.";
        }
    }

    protected string ObterCorFundoCargo(string cargo)
    {
        if (string.IsNullOrEmpty(cargo)) return "#f1f5f9";
        return cargo.ToLower() switch
        {
            "gerente" => "#e0f2fe",
            "vendedor" => "#dcfce7",
            "estoquista" => "#f3e8ff",
            _ => "#f1f5f9"
        };
    }

    protected string ObterCorTextoCargo(string cargo)
    {
        if (string.IsNullOrEmpty(cargo)) return "#475569";
        return cargo.ToLower() switch
        {
            "gerente" => "#0369a1",
            "vendedor" => "#15803d",
            "estoquista" => "#6b21a8",
            _ => "#475569"
        };
    }
}