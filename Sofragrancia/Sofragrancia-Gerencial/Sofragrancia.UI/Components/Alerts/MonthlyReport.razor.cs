using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Sofragrancia.Shared.Dtos;

namespace Sofragrancia.UI.Components.Alerts;

public partial class MonthlyReport
{ 
    // O DTO unificado que agora gerencia o estado isolado desta aba
    protected MonthlyReportDto Model { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        // 1. Carrega o esqueleto inicial com as seções do relatório disponíveis
        Model.Sections = ObterSecoesPadrao();

        // 2. Define os valores iniciais padrão para a tela
        Model.EmailDestinatario = "diretoria@sofragrancia.com.br";
        Model.DataInicialRelatorio = DateTime.Today.AddMonths(-1);
        Model.DataFinalRelatorio = DateTime.Today;
    }

    protected async Task EnviarRelatorioCustomizado()
    {
        try
        {
            // Aqui os dados já estarão 100% atualizados via @bind do HTML
            // TODO: Chamar o seu serviço de API enviando o DTO completo
            // await SeuServico.DispararRelatorioCustomizadoAsync(Model);

            System.Diagnostics.Debug.WriteLine($"[Relatório] Disparado com sucesso para: {Model.EmailDestinatario}");
            System.Diagnostics.Debug.WriteLine($"[Relatório] Período: {Model.DataInicialRelatorio:dd/MM/yyyy} até {Model.DataFinalRelatorio:dd/MM/yyyy}");
            System.Diagnostics.Debug.WriteLine($"[Relatório] Total de seções incluídas: {Model.Sections.FindAll(s => s.Ativo).Count}");
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[Erro] Falha ao enviar relatório customizado: {ex.Message}");
        }
    }

    private List<MonthlyReportSectionDto> ObterSecoesPadrao()
    {
        return new()
        {
            new() { Id = "dashboard", Titulo = "Dashboard Geral", Icone = "📊" },
            new() { Id = "cotas", Titulo = "Cotas e Atingimento", Icone = "🎯" },
            new() { Id = "faturamento", Titulo = "Faturamento Detalhado", Icone = "💰" },
            new() { Id = "ranking", Titulo = "Ranking Vendedores", Icone = "🏆" },
            new() { Id = "produtos", Titulo = "Performance Produtos", Icone = "📦" },
            new() { Id = "clientes", Titulo = "Análise de Clientes", Icone = "👥" },
            new() { Id = "cancelamentos", Titulo = "Cancelamentos", Icone = "❌" },
            new() { Id = "estoque", Titulo = "Posição de Estoque", Icone = "🏭" },
            new() { Id = "alertas", Titulo = "Alertas Disparados", Icone = "🛡️" },
            new() { Id = "precos", Titulo = "Variação de Preços", Icone = "💹" },
            new() { Id = "bi", Titulo = "15 Análises de BI", Icone = "📈" },
            new() { Id = "projecao", Titulo = "Projeções Próx. Mês", Icone = "🔮" }
        };
    }
}