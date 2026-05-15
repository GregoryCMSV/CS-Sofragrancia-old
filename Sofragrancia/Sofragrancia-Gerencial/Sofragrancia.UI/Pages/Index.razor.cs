using Sofragrancia.UI.Components;
using Sofragrancia.UI.Models;

namespace Sofragrancia.UI.Pages
{
    public partial class Index
    {
        private List<KpiModel> PinnedKpis { get; set; } = new();
        private List<ChartItemModel> DadosFaturamento { get; set; } = new();
        private List<DonutChartItemModel> DadosMixVendas { get; set; } = new();

        protected override void OnInitialized()
        {
            InitializeBarChart();

            PinnedKpis = new List<KpiModel>{
                new KpiModel { Title = "% Cota Geral", Value = Math.Round(DadosFaturamento.Sum(f => GetValidValue(f.ValueText))/DateTime.Today.Day,2).ToString(), Icon = "🎯", ColorClass = "gold", HasProgress = true, ProgressPercentage = Math.Round(DadosFaturamento.Sum(f => GetValidValue(f.ValueText))/DateTime.Today.Day,2), ProgressTextLeft = "Realizado", ProgressTextRight = "Meta: 100%" },
                new KpiModel { Title = "Faturamento Bruto", Value = Math.Round(DadosFaturamento.Sum(f => GetValidValue(f.ValueText)),2).ToString(), Icon = "💲", ColorClass = "blue", ChangeText = "▲ +12,3% vs meta", ChangeClass = "up" },
                new KpiModel { Title = "Ticket Médio", Value = Math.Round(DadosFaturamento.Average(f => GetValidValue(f.ValueText)),2).ToString(), Icon = "🧾", ColorClass = "purple", ChangeText = "▲ +3,2% vs mês ant.", ChangeClass = "up" },
                new KpiModel { Title = "Pedidos Pendentes", Value = "14", Icon = "⏳", ColorClass = "red", ChangeText = "▼ -3 vs ontem", ChangeClass = "down" }
            };


            DadosMixVendas = new List<DonutChartItemModel>{
                new DonutChartItemModel { Label = "SOXERO", ValueText = "22%", Percentage = 22, ColorHex = "#3498db" },
                new DonutChartItemModel { Label = "SOHODOR", ValueText = "18%", Percentage = 18, ColorHex = "#2ecc71" },
                new DonutChartItemModel { Label = "SOLAVANDO", ValueText = "15%", Percentage = 15, ColorHex = "#e6a817" },
                new DonutChartItemModel { Label = "SONAREZA", ValueText = "15%", Percentage = 15, ColorHex = "#9b59b6" },
                new DonutChartItemModel { Label = "SOFRENCIA", ValueText = "12%", Percentage = 12, ColorHex = "#e67e22" },
                new DonutChartItemModel { Label = "SOSENTE", ValueText = "9%", Percentage = 9, ColorHex = "#1abc9c" },
                new DonutChartItemModel { Label = "Outros", ValueText = "9%", Percentage = 9, ColorHex = "#e74c3c" }
            };
        }

        private void InitializeBarChart()
        {
            DadosFaturamento = new List<ChartItemModel>();
            Random random = new Random();

            for(int i = 0; i < DateTime.Today.Day; i++)
            {
                DadosFaturamento.Add(new() { Label = (i + 1).ToString(), ValueText = (random.NextDouble() * 100).ToString("0.0"), ColorClass = "primary" });
            }

            var total = DadosFaturamento.Max(f => double.TryParse(f.ValueText, out double valor) ? valor : 1);

            foreach(var barItem in DadosFaturamento)
            {
                barItem.Percentage = Math.Round(GetValidValue(barItem.ValueText) / total * 100,2);
            }

            DadosFaturamento.FindAll(f => f.Percentage >= 90).ForEach(f => f.ColorClass = "success");
            DadosFaturamento.FindAll(f => f.Percentage <= 45).ForEach(f => f.ColorClass = "warning");


            DadosFaturamento.ForEach(d => Console.WriteLine($"Porcentagem do dia:{d.Label} é: {d.Percentage}"));

        }

        private double GetValidValue(string valorTexto) => double.TryParse(valorTexto, out double valor) ? valor : 0;
        

    }
}

