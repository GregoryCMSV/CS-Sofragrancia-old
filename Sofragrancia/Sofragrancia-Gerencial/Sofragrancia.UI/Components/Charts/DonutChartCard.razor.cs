using Microsoft.AspNetCore.Components;
using Sofragrancia.UI.Models;

namespace Sofragrancia.UI.Components.Charts
{
    public partial class DonutChartCard
    {
        [Parameter] public string Title { get; set; }
        [Parameter] public string Subtitle { get; set; }
        [Parameter] public string TotalValue { get; set; }
        [Parameter] public string TotalLabel { get; set; }
        [Parameter] public List<DonutChartItemModel> Items { get; set; } = new();


        private string GerarConicGradient()
        {
            if (Items == null || !Items.Any())
                return "conic-gradient(#ccc 0% 100%)";

            var pedacos = new List<string>();
            double inicioAtual = 0;

            foreach (var item in Items)
            {
                double fimAtual = inicioAtual + item.Percentage;


                string formatInicio = inicioAtual.ToString(System.Globalization.CultureInfo.InvariantCulture);
                string formatFim = fimAtual.ToString(System.Globalization.CultureInfo.InvariantCulture);

                pedacos.Add($"{item.ColorHex} {formatInicio}% {formatFim}%");

                inicioAtual = fimAtual;
            }

            return $"conic-gradient({string.Join(", ", pedacos)})";
        }
    }
}
