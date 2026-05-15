using Microsoft.AspNetCore.Components;
using Sofragrancia.UI.Models;

namespace Sofragrancia.UI.Components.Charts
{
    public partial class BarChartCard
    {
        [Parameter] public string Title { get; set; }
        [Parameter] public string Subtitle { get; set; }
        [Parameter] public bool IsHorizontal { get; set; }
        [Parameter] public List<ChartItemModel> Items { get; set; } = new();
    }
}
