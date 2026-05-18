using Microsoft.AspNetCore.Components;
using Sofragrancia.UI.Components;
using Sofragrancia.UI.Components.Settings;

namespace Sofragrancia.UI.Pages
{
    public partial class Settings : ComponentBase
    {
        protected string abaAtiva = "perfil";

        protected List<TabNavigation.TabItem> tabs = new()
        {
            new()
            {
                Id = "perfil",
                Title = "Meu Perfil",
                Icon = "👤"
            },

            new()
            {
                Id = "usuarios",
                Title = "Cadastrar Usuário",
                Icon = "➕"
            }
        };


        protected PerfilModel Perfil { get; set; } = new PerfilModel();
        protected CadastroUsuarioModel NovoUsuario { get; set; } = new CadastroUsuarioModel();

        protected void AlternarAba(string novaAba)
        {
            abaAtiva = novaAba;
            //LimparTodasAsMensagens();
            StateHasChanged();
        }
    }
}