using Microsoft.AspNetCore.Components;
namespace Sofragrancia.UI.Components.Settings;


public partial class NewUser
{
        [Parameter]
        public CadastroUsuarioModel NovoUsuario { get; set; } = new CadastroUsuarioModel();
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
                MensagemSucessoCadastro = $"Colaborador '{NovoUsuario.Nome}' cadastrado com sucesso como '{NovoUsuario.PerfilAcesso}'!";
                NovoUsuario = new CadastroUsuarioModel(); 
            }
            catch (Exception)
            {
                MensagemErroCadastro = "Falha técnica ao tentar salvar o colaborador no banco de dados.";
            }
        }

        protected void LimparFormularioCadastro()
        {
            NovoUsuario = new CadastroUsuarioModel();
            LimparTodasAsMensagens();
        }

        private void LimparTodasAsMensagens()
        {
            MensagemSucessoCadastro = string.Empty;
            MensagemErroCadastro = string.Empty;
        }

        
        protected string MensagemSucessoCadastro { get; set; } = string.Empty;
        protected string MensagemErroCadastro { get; set; } = string.Empty;
}