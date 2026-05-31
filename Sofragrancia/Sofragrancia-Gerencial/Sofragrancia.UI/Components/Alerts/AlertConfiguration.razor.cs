using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json; // Lib para consumo do GetFromJsonAsync
using Sofragrancia.Shared.Dtos;
using Sofragrancia.UI.Services; 

namespace Sofragrancia.UI.Components.Alerts;

public partial class AlertConfiguration
{ 
    [Inject] protected HttpService HttpService { get; set; } = default!;

    // O DTO unificado que agora gerencia o estado da tela inteira
    protected AlertConfigurationDto Model { get; set; } = new();

    // Dicionário amigável por extenso para a nova coluna da tabela
    protected readonly Dictionary<string, string> OperadoresDisponiveis = new()
    {
        { "<=", "Menor ou igual que" },
        { ">=", "Maior ou igual que" },
        { "<",  "Menor que" },
        { ">",  "Maior que" },
        { "==", "Igual a" },
        { "!=", "Diferente de" }
    };

    protected override async Task OnInitializedAsync()
    {
        // [INTEGRACAO_API]
        // Model = await HttpService.GetFromJsonAsync<AlertConfigurationDto>("api/configuracoes/alertas") ?? new();

        Model = CarregarDadosIniciais();
    }

    protected async Task SalvarConfiguracoes()
    {   
        try
        {
            // Simulação de delay para feedback visual de salvamento na apresentação
            await Task.Delay(1000); 


            // [INTEGRACAO_API]
            /*
            var response = await HttpService.PostAsync("api/configuracoes/alertas", Model);
            if (!response.IsSuccessStatusCode) 
            {
                // Tratar erro de salvamento aqui se necessário
                return;
            }
            */

            
            System.Diagnostics.Debug.WriteLine($"[Sucesso] Configurações enviadas para o banco!");
            System.Diagnostics.Debug.WriteLine($"E-mail ativo: {Model.EmailAtivo} | Destinatário: {Model.EmailDestinatario}");
            System.Diagnostics.Debug.WriteLine($"Total de indicadores monitorados: {Model.Indicators.Count}");
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[Erro] Falha ao salvar no banco: {ex.Message}");
        }
    }

    private AlertConfigurationDto CarregarDadosIniciais()
    {
        return new AlertConfigurationDto
        {
            EmailDestinatario = "gerente@sofragrancia.com.br",
            EmailAtivo = true,
            HorarioEnvio = new TimeOnly(17, 0, 0),
            FaturamentoSegunda = true,
            FaturamentoQuarta = true,
            FaturamentoSexta = true,
            Indicators = new()
            {
                new() { Id = "estoque_critico", Title = "🏭 Estoque Crítico", Operator = "<=", Unit = "un.", Value = 50.0, IsActive = true },
                new() { Id = "cancelamento", Title = "❌ Taxa de Cancelamento", Operator = "<=", Unit = "%", Value = 5.0, IsActive = true },
                new() { Id = "cobertura_cota", Title = "🎯 Cobertura de Cota", Operator = "<=", Unit = "%", Value = 40.0, IsActive = true },
                new() { Id = "meta_risco", Title = "📈 Meta Mensal em Risco", Operator = "<=", Unit = "% meta", Value = 90.0, IsActive = true },
                new() { Id = "cliente_inativo", Title = "🔄 Cliente Inativo", Operator = ">=", Unit = "dias", Value = 20.0, IsActive = true }
            }
        };
    }
}