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

        // Validação local rápida (evita fazer requisição desnecessária se o form estiver vazio)
        if (string.IsNullOrWhiteSpace(NovoUsuario.Nome) || string.IsNullOrWhiteSpace(NovoUsuario.Email))
        {
            MensagemErroCadastro = "Por favor, preencha todos os campos obrigatórios.";
            return;
        }

        try
        {
            // ⏳ Simulação de delay mantida para a apresentação local
            await Task.Delay(1000); 

            // 🚀 Faz a chamada real para a rota corrigida
            var response = await HttpService.PostAsync("api/usuario/cadastro", NovoUsuario);

            if (response.IsSuccessStatusCode)
            {
                // 🟢 SUCESSO: Lê o JSON gerado pelo "return Ok(new { Mensagem = ... })"
                var resultadoSucesso = await response.Content.ReadFromJsonAsync<RespostaSucessoApiDto>();
                
                MensagemSucessoCadastro = resultadoSucesso?.Mensagem ?? "Colaborador cadastrado com sucesso!";
                NovoUsuario = new UserRegistrationDto(); // Limpa o formulário
            }
            else
            {
                // 🔴 ERRO: Lê o JSON gerado pelo "return StatusCode(403)" ou "return BadRequest(new { Erro = ... })"
                var resultadoErro = await response.Content.ReadFromJsonAsync<RespostaErroApiDto>();
                
                MensagemErroCadastro = resultadoErro?.Erro ?? "Não foi possível salvar o colaborador no servidor.";
            }
        }
        catch (Exception ex)
        {
            MensagemErroCadastro = $"Falha técnica ao tentar se comunicar com o servidor: {ex.Message}";
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