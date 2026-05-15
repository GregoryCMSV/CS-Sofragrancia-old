using Microsoft.AspNetCore.Components;

namespace Sofragrancia.UI.Components
{
    public partial class Topbar
    {
        // Injeta o serviço de navegação
        [Inject] public NavigationManager Navigation { get; set; } = default!;
        [Parameter] public string Title { get; set; } = "Dashboard Geral";
        [Parameter] public string Breadcrumb { get; set; } = "Início > Dashboard";

        private DateTime DataInicio { get; set; } = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
        private DateTime DataFim { get; set; } = DateTime.Today;

        // Controle do menu do usuário
        private bool exibirMenu = false;
        private void ToggleMenu() => exibirMenu = !exibirMenu;

        private void Filtrar()
        {
            Console.WriteLine($"Filtrando de {DataInicio.ToShortDateString()} até {DataFim.ToShortDateString()}");
        }

        // Método para navegar e fechar o menu
        private void IrPara(string rota)
        {
            exibirMenu = false;
            Navigation.NavigateTo(rota);
        }
    }
}
