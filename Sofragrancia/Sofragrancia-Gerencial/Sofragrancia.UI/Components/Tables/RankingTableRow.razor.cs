using Microsoft.AspNetCore.Components;
using Sofragrancia.UI.Models;

namespace Sofragrancia.UI.Components.Tables
{
    public partial class RankingTableRow
    {
        [Parameter] public RankingItem Item { get; set; } = new();
        private string GetBarColor() => Item.StatusClass switch
        {
            "success" => "#2ecc71",
            "danger" => "#e74c3c",
            _ => "#e6a817"
        };

        private string GetTextColor() => Item.StatusClass switch
        {
            "success" => "text-green",
            "danger" => "text-red",
            _ => "text-orange"
        };

        private string GetStatusIcon() => Item.StatusClass switch
        {
            "success" => "🟢",
            "danger" => "🔴",
            _ => "🟡"
        };
    }
}
