using Microsoft.AspNetCore.Components;
using Sofragrancia.UI.Services;
using Sofragrancia.Shared.Dtos;

namespace Sofragrancia.UI.Components
{
    public partial class Topbar
    {
        // Injeta o serviço de navegação
        [Inject] public NavigationService NavigationService{ get; set; } = default!;
        [Inject] protected TokenService TokenService { get; set; } = default!;
        [Parameter] public string Title { get; set; } = "Dashboard Geral";
        [Parameter] public string Breadcrumb { get; set; } = "Início > Dashboard";

        protected UserMetaData? UsuarioLogado { get; set; }

        private DateTime DataInicio { get; set; } = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
        private DateTime DataFim { get; set; } = DateTime.Today;

        // Controle do menu do usuário
        private bool exibirMenu = false;

        protected override async Task OnInitializedAsync()
        {
            UsuarioLogado = await TokenService.ObterDadosUsuarioLogadoAsync();
        }

        // Função auxiliar para gerar as duas primeiras letras do nome no Avatar (Ex: "João Silva" -> "JS")
        protected string ObterIniciais(string? nome)
        {
            if (string.IsNullOrWhiteSpace(nome)) return "??";
            var partes = nome.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (partes.Length == 1) return partes[0].Substring(0, Math.Min(2, partes[0].Length)).ToUpper();
            return $"{partes[0][0]}{partes[^1][0]}".ToUpper();
        }
        
        private void ToggleMenu() => exibirMenu = !exibirMenu;

        private void Filtrar()
        {
            Console.WriteLine($"Filtrando de {DataInicio.ToShortDateString()} até {DataFim.ToShortDateString()}");
        }

        // Método para navegar e fechar o menu
        private async Task IrPara(string rota)
        {
            exibirMenu = false;
            await NavigationService.NavigateAsync(rota);
        }

        protected async Task Logout()
        {
            await TokenService.RemoverTokenAsync();
            await NavigationService.NavigateAsync("login");
        }
    }
}
