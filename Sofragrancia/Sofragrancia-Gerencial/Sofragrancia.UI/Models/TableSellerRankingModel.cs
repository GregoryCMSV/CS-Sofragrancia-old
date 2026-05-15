namespace Sofragrancia.UI.Models
{
    public class RankingItem
    {
        public int Rank { get; set; }
        public int Codigo { get; set; }
        public string Vendedor { get; set; }
        public int CotaQtd { get; set; }
        public int VendidoQtd { get; set; }
        public double PercentualAtingimento { get; set; }
        public decimal Faturamento { get; set; }
        public string StatusNome { get; set; } 
        public string StatusClass { get; set; }
    }


}
