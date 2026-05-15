using Microsoft.AspNetCore.Components;
using Sofragrancia.UI.Models;

namespace Sofragrancia.UI.Components.Charts
{
    public partial class LineChartCard
    {
        [Parameter] public string Title { get; set; }
        [Parameter] public string Subtitle { get; set; }
        [Parameter] public List<ChartItemModel> Items { get; set; } = new();

        private double GetX(int index)
        {
            if (Items.Count <= 1) return 500;
            return (index * 1000.0) / (Items.Count - 1);
        }

        private double GetY(double percentage)
        {
            var perc = Math.Max(0, Math.Min(100, percentage));
            return 300 - (perc * 3.0);
        }

        private string GetPolylinePoints()
        {
            var points = new List<string>();
            for (int i = 0; i < Items.Count; i++)
            {
                var x = GetX(i).ToString(System.Globalization.CultureInfo.InvariantCulture);
                var y = GetY(Items[i].Percentage).ToString(System.Globalization.CultureInfo.InvariantCulture);
                points.Add($"{x},{y}");
            }
            return string.Join(" ", points);
        }

       
    }
}
