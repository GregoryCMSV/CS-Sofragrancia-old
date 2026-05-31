using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using Sofragrancia.Shared.Dtos;
using Sofragrancia.UI.Services;

namespace Sofragrancia.UI.Components.Alerts;

public partial class MonthlyReport
{ 
    [Inject] protected HttpService HttpService { get; set; } = default!;

    // O DTO unificado que agora gerencia o estado isolado desta aba
    protected MonthlyReportDto Model { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        // [INTEGRACAO_API]
        // var dadosSalvos = await HttpService.GetFromJsonAsync<MonthlyReportDto>("api/relatorios/configuracao-padrao");
        // if (dadosSalvos is not null) { Model = dadosSalvos; return; }

        // 1. Carrega o esqueleto inicial com as seções do relatório disponíveis
        Model.Sections = ObterSecoesPadrao();

        // 2. Define os valores iniciais padrão para a tela (Mock de Apresentação)
        Model.EmailDestinatario = "diretoria@sofragrancia.com.br";
        Model.DataInicialRelatorio = DateTime.Today.AddMonths(-1);
        Model.DataFinalRelatorio = DateTime.Today;
    }

    protected async Task EnviarRelatorioCustomizado()
    {
        try
        {
            // Simulação de delay para dar o feedback visual de carregamento na apresentação
            await Task.Delay(1200); 

            // [INTEGRACAO_API]
            /*
            var response = await HttpService.PostAsync("api/relatorios/disparar-customizado", Model);
            if (!response.IsSuccessStatusCode) 
            {
                // Tratar erro de envio se necessário
                return;
            }
            */

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