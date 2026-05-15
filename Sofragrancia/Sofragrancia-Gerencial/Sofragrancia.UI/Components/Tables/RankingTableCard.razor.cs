using Sofragrancia.UI.Models;

namespace Sofragrancia.UI.Components.Tables
{
    public partial class RankingTableCard
    {
        private List<RankingItem> VendedoresRanking { get; set; } = new();

        protected override void OnInitialized()
        {
            VendedoresRanking = new List<RankingItem>
        {
            new RankingItem { Rank = 1, Codigo = 510, Vendedor = "BENJAMIN FRANKLIN", CotaQtd = 3392, VendidoQtd = 3450, PercentualAtingimento = 101.7, Faturamento = 34500, StatusNome = "Acima", StatusClass = "success" },
            new RankingItem { Rank = 2, Codigo = 580, Vendedor = "INACIO DE LOYOLA", CotaQtd = 3392, VendidoQtd = 3380, PercentualAtingimento = 99.6, Faturamento = 33800, StatusNome = "Meta", StatusClass = "success" },
            new RankingItem { Rank = 3, Codigo = 560, Vendedor = "GIULIANO GEMMA", CotaQtd = 3392, VendidoQtd = 3200, PercentualAtingimento = 94.3, Faturamento = 32000, StatusNome = "Meta", StatusClass = "success" },
            new RankingItem { Rank = 4, Codigo = 540, Vendedor = "EDWARD NORTON", CotaQtd = 3392, VendidoQtd = 3100, PercentualAtingimento = 91.4, Faturamento = 31000, StatusNome = "Meta", StatusClass = "success" },
            new RankingItem { Rank = 5, Codigo = 500, Vendedor = "ADAM SMITH", CotaQtd = 3392, VendidoQtd = 2890, PercentualAtingimento = 85.2, Faturamento = 28900, StatusNome = "Atenção", StatusClass = "warning" },
            new RankingItem { Rank = 6, Codigo = 530, Vendedor = "DONALD TRUMP", CotaQtd = 3392, VendidoQtd = 2710, PercentualAtingimento = 79.9, Faturamento = 27100, StatusNome = "Atenção", StatusClass = "warning" },
            new RankingItem { Rank = 7, Codigo = 570, Vendedor = "HAROLD FLINT", CotaQtd = 3392, VendidoQtd = 2600, PercentualAtingimento = 76.7, Faturamento = 26000, StatusNome = "Atenção", StatusClass = "warning" },
            new RankingItem { Rank = 8, Codigo = 550, Vendedor = "FRANCIS BACON", CotaQtd = 3392, VendidoQtd = 2050, PercentualAtingimento = 60.4, Faturamento = 20500, StatusNome = "Atenção", StatusClass = "warning" },
            new RankingItem { Rank = 9, Codigo = 590, Vendedor = "JEAN MICHEL JARRE", CotaQtd = 3392, VendidoQtd = 1800, PercentualAtingimento = 53.1, Faturamento = 18000, StatusNome = "Crítico", StatusClass = "danger" },
            new RankingItem { Rank = 10, Codigo = 520, Vendedor = "CINDY CRAWFORD", CotaQtd = 3392, VendidoQtd = 1200, PercentualAtingimento = 35.4, Faturamento = 12000, StatusNome = "Crítico", StatusClass = "danger" }
        };
        }

        private void ExportarDados()
        {
            Console.WriteLine("Botão de exportar clicado!");
        }
    }
}
