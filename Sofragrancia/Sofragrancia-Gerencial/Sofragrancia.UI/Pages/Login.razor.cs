using Microsoft.AspNetCore.Components;

namespace Sofragrancia.UI.Pages
{
    public partial class Login
    {
        [Inject]
        protected NavigationManager Navigation { get; set; } = default!;

        protected string Email { get; set; } = string.Empty;
        protected string Senha { get; set; } = string.Empty;
        protected bool LembrarMe { get; set; }
        protected string MensagemErro { get; set; } = string.Empty;

        protected void ExecutarLogin()
        {
            if (Email == "admin" && Senha == "admin")
            {
                MensagemErro = string.Empty;
                Navigation.NavigateTo("/home");
            }
            else
            {
                MensagemErro = "Usuário ou senha inválidos. Tente novamente.";
            }
        }
    }
}