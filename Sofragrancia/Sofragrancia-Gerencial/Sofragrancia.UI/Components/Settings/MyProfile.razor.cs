using Microsoft.AspNetCore.Components;
namespace Sofragrancia.UI.Components.Settings;

public partial class MyProfile
{
        [Parameter]
        public PerfilModel Perfil { get; set; } = new PerfilModel();
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

        protected void SalvarPerfil()
        {

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
                MensagemSucessoPerfil = "Sua senha foi atualizada com sucesso!";
                AlternarMenuSenha(); 
            }
            catch (Exception)
            {
                MensagemErroPerfil = "Erro ao tentar atualizar sua senha de acesso.";
            }
        }

              // Métodos Auxiliares de Estilização Dinâmica para os Cargos do ERP
        protected string ObterCorFundoCargo(string cargo)
        {
            return cargo.ToLower() switch
            {
                "gerente" => "#e0f2fe",    // Azul Claro Corporativo
                "vendedor" => "#dcfce7",   // Verde Comercial
                "estoquista" => "#f3e8ff", // Roxo Operacional
                _ => "#f1f5f9"
            };
        }

        protected string ObterCorTextoCargo(string cargo)
        {
            return cargo.ToLower() switch
            {
                "gerente" => "#0369a1",    // Azul Escuro
                "vendedor" => "#15803d",   // Verde Escuro
                "estoquista" => "#6b21a8", // Roxo Escuro
                _ => "#475569"
            };
        }

        protected override void OnInitialized()
        {
            // Simula os dados consolidados vindos da sessão do usuário logado
            Perfil.Nome = "Benjamin Franklin";
            Perfil.Email = "gerente@sofragrancia.com.br";
            Perfil.PerfilAcesso = "Gerente"; 
        }


        protected bool exibirMenuSenha = false;

        protected string MensagemSucessoPerfil { get; set; } = string.Empty;
        protected string MensagemErroPerfil { get; set; } = string.Empty;

}