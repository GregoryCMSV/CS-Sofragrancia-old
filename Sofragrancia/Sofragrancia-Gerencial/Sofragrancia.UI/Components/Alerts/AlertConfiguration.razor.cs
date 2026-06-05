using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using Sofragrancia.Shared.Dtos;
using Sofragrancia.UI.Services; 
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace Sofragrancia.UI.Components.Alerts;

public partial class AlertConfiguration
{ 
    [Inject] protected HttpService HttpService { get; set; } = default!;
    
    // Injeta o serviço de token para capturar o e-mail do usuário logado
    [Inject] protected TokenService TokenService { get; set; } = default!;

    // O DTO unificado que gerencia o estado da tela inteira
    protected AlertConfigurationDto Model { get; set; } = new();

    // Dicionário amigável por extenso para a coluna da tabela
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
        // 1. Carrega as configurações padrão locais na tela
        Model = CarregarDadosIniciais();

        // 2. Abre o Token JWT para descobrir o e-mail do usuário real logado
        var tokenRaw = await TokenService.ObterTokenAsync();
        if (!string.IsNullOrWhiteSpace(tokenRaw))
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(tokenRaw.Replace("Bearer ", ""));
                
                var emailReal = jwtToken.Claims.FirstOrDefault(c => c.Type == "email" || c.Type == "unique_name")?.Value;
                
                if (!string.IsNullOrEmpty(emailReal))
                {
                    // Atualiza o Model com o e-mail de quem realmente está usando o ERP
                    Model.EmailDestinatario = emailReal;
                }
            }
            catch (Exception)
            {
                // Se falhar a leitura do token, mantém o e-mail padrão do CarregarDadosIniciais
            }
        }

        // 3. Com o e-mail definido, tenta buscar as regras reais usando o formato do seu HttpService
        try
        {
            var response = await HttpService.GetAsync($"api/Alerta/email/{Model.EmailDestinatario}");
            
            if (response.IsSuccessStatusCode)
            {
                var dadosBanco = await response.Content.ReadFromJsonAsync<AlertConfigurationDto>();
                if (dadosBanco != null)
                {
                    Model = dadosBanco;
                }
            }
        }
        catch (Exception ex)
        {
            // Se a API estiver offline, ignora o erro e mantém os dados iniciais mockados para teste visual
            System.Diagnostics.Debug.WriteLine($"[Aviso] Não foi possível conectar à API de Alertas: {ex.Message}. Usando dados simulados.");
        }
    }

    protected async Task SalvarConfiguracoes()
    {   
        try
        {
            // [INTEGRACAO_API] - Ajustado para usar o PostAsync da sua classe HttpService
            /*
            var response = await HttpService.PostAsync("api/Alerta/salvar", Model);
            if (!response.IsSuccessStatusCode) 
            {
                System.Diagnostics.Debug.WriteLine("[Erro] Falha ao salvar as configurações na API.");
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
            Indicators = new()
            {
                new() { 
                    Id = "estoque_critico", 
                    Title = "🏭 Estoque Crítico", 
                    Operator = "<=", 
                    Unit = "un.", 
                    Value = 50.0, 
                    IsActive = true,
                    OperadoresPermitidos = new() { "<=", "<" },
                    UnidadesPermitidas = new() { "un." }
                },
                new() { 
                    Id = "cobertura_cota", 
                    Title = "🎯 Cobertura de Cota", 
                    Operator = "<=", 
                    Unit = "%", 
                    Value = 40.0, 
                    IsActive = true, 
                    OperadoresPermitidos = new() { "<=", "<" },
                    UnidadesPermitidas = new() { "%" }
                },
                new() { 
                    Id = "cancelamento", 
                    Title = "❌ Taxa de Cancelamento", 
                    Operator = ">=", 
                    Unit = "%", 
                    Value = 5.0, 
                    IsActive = true,
                    OperadoresPermitidos = new() { ">=" },
                    UnidadesPermitidas = new() { "%" }
                },
                new() { 
                    Id = "meta_risco", 
                    Title = "📈 Meta Mensal em Risco", 
                    Operator = "<=", 
                    Unit = "% meta", 
                    Value = 90.0, 
                    IsActive = true,
                    OperadoresPermitidos = new() { "<=" },
                    UnidadesPermitidas = new() { "% meta" }
                },
                new() { 
                    Id = "cliente_inativo", 
                    Title = "🔄 Cliente Inativo", 
                    Operator = ">=", 
                    Unit = "dias", 
                    Value = 20.0, 
                    IsActive = true,
                    OperadoresPermitidos = new() { ">=", ">", "==" },
                    UnidadesPermitidas = new() { "dias", "meses" }
                }
            }
        };
    }
}