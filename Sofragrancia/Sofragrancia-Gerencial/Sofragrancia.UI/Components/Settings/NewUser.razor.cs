using System;
using System.Threading.Tasks; // Adicionado para suportar chamadas async de API
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using Sofragrancia.Shared.Dtos;
using Sofragrancia.UI.Services; 

namespace Sofragrancia.UI.Components.Settings;

public partial class NewUser
{
    [Inject] protected HttpService HttpService { get; set; } = default!;

    protected UserRegistrationDto NovoUsuario { get; set; } = new();
    protected string MensagemSucessoCadastro { get; set; } = string.Empty;
    protected string MensagemErroCadastro { get; set; } = string.Empty;

    protected async Task SalvarNovoUsuario()
    {
        LimparTodasAsMensagens();

        if (string.IsNullOrWhiteSpace(NovoUsuario.Nome) || string.IsNullOrWhiteSpace(NovoUsuario.Email))
        {
            MensagemErroCadastro = "Por favor, preencha todos os campos obrigatórios.";
            return;
        }

        try
        {
            // Simulação de delay para o feedback visual de salvamento na apresentação
            await Task.Delay(1000); 


            // [INTEGRACAO_API]
            /*
            var response = await HttpService.PostAsync("api/usuario/cadastrar", NovoUsuario);
            if (!response.IsSuccessStatusCode) 
            {
                MensagemErroCadastro = "Não foi possível salvar o colaborador no servidor.";
                return;
            }
            */

            // [INTEGRACAO_API]
            MensagemSucessoCadastro = $"Colaborador '{NovoUsuario.Nome}' cadastrado com sucesso como '{NovoUsuario.PerfilAcesso}'!";
            NovoUsuario = new UserRegistrationDto();
        }
        catch (Exception)
        {
            MensagemErroCadastro = "Falha técnica ao tentar salvar o colaborador no banco de dados.";
        }
    }

    protected void LimparFormularioCadastro()
    {
        NovoUsuario = new UserRegistrationDto();
        LimparTodasAsMensagens();
    }

    private void LimparTodasAsMensagens()
    {
        MensagemSucessoCadastro = string.Empty;
        MensagemErroCadastro = string.Empty;
    }
}