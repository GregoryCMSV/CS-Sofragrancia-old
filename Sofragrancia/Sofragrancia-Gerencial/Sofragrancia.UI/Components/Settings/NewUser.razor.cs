using System;
using Sofragrancia.Shared.Dtos;

namespace Sofragrancia.UI.Components.Settings;

public partial class NewUser
{
    // Estado isolado usando o novo DTO da Shared
    protected UserRegistrationDto NovoUsuario { get; set; } = new();

    protected string MensagemSucessoCadastro { get; set; } = string.Empty;
    protected string MensagemErroCadastro { get; set; } = string.Empty;

    protected void SalvarNovoUsuario()
    {
        LimparTodasAsMensagens();

        if (string.IsNullOrWhiteSpace(NovoUsuario.Nome) || string.IsNullOrWhiteSpace(NovoUsuario.Email))
        {
            MensagemErroCadastro = "Por favor, preencha todos os campos obrigatórios.";
            return;
        }

        try
        {
            // TODO: Integrar chamada à API/Supabase aqui futuramente
            MensagemSucessoCadastro = $"Colaborador '{NovoUsuario.Nome}' cadastrado com sucesso como '{NovoUsuario.PerfilAcesso}'!";
            NovoUsuario = new UserRegistrationDto(); // Reseta o form limpo
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