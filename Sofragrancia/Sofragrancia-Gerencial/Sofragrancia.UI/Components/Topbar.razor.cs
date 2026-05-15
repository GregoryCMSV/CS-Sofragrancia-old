using Microsoft.AspNetCore.Components;

namespace Sofragrancia.UI.Components
{
    public partial class Topbar
    {
        [Parameter] public string Title { get; set; } = "Dashboard Geral";
        [Parameter] public string Breadcrumb { get; set; } = "Início > Dashboard";

        private DateTime DataInicio { get; set; } = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
        private DateTime DataFim { get; set; } = DateTime.Today;

        private void Filtrar()
        {
            Console.WriteLine($"Filtrando de {DataInicio.ToShortDateString()} até {DataFim.ToShortDateString()}");
        }
    }
}
