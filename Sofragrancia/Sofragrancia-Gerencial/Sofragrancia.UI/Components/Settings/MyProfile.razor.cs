using System;
using System.Threading.Tasks; 
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using Sofragrancia.Shared.Dtos;
using Sofragrancia.UI.Services; 

namespace Sofragrancia.UI.Components.Settings;

public partial class MyProfile
{
    [Inject] protected HttpService HttpService { get; set; } = default!;

    // Estado isolado usando o novo DTO da Shared
    protected UserProfileDto Perfil { get; set; } = new();

    protected bool exibirMenuSenha = false;
    protected string MensagemSucessoPerfil { get; set; } = string.Empty;
    protected string MensagemErroPerfil { get; set; } = string.Empty;

    protected override async Task OnInitializedAsync()
    {

        // [INTEGRACAO_API]
        // Perfil = await HttpService.GetFromJsonAsync<UserProfileDto>("api/usuario/perfil") ?? new();


        Perfil.Nome = "Benjamin Franklin";
        Perfil.Email = "gerente@sofragrancia.com.br";
        Perfil.PerfilAcesso = "Gerente";
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
            // Simulação de delay para o feedback visual de carregamento na apresentação
            await Task.Delay(1000); 

            // [INTEGRACAO_API]
            /*
            var response = await HttpService.PostAsync("api/usuario/alterar-senha", Perfil);
            if (!response.IsSuccessStatusCode) 
            {
                MensagemErroPerfil = "Não foi possível atualizar a senha no servidor.";
                return;
            }
            */

            MensagemSucessoPerfil = "Sua senha foi updated com sucesso!";
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