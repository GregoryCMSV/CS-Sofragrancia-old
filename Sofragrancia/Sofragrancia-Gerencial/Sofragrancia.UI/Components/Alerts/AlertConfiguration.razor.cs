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
    [Inject] protected TokenService TokenService { get; set; } = default!;

    // O DTO unificado que gerencia o estado da tela inteira
    protected AlertConfigurationDto Model { get; set; } = new();

    // Guarda o Id do AlertaHeader que veio do banco — necessário para o PATCH
    private int _headerIdBanco = 0;

    // Guarda os Ids dos AlertaConfigUser por IdAlertaBase — necessário para o PATCH de cada linha
    // Chave: IdAlertaBase (ex: 1 = EstoqueMin), Valor: Id da linha na tabela alertaconfigurado
    private Dictionary<int, int> _alertaConfigIds = new();

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

    // -------------------------------------------------------------------------
    // Tabelas de conversão: int do banco <-> string da tela
    // -------------------------------------------------------------------------

    // Trigger: int -> operador string
    private static readonly Dictionary<int, string> TriggerParaOperador = new()
    {
        { 1, "<=" },
        { 2, "<"  },
        { 4, ">=" },
        { 5, ">"  }
    };

    // Operador string -> int (inverso, para montar o PATCH)
    private static readonly Dictionary<string, int> OperadorParaTrigger = new()
    {
        { "<=", 1 },
        { "<",  2 },
        { ">=", 4 },
        { ">",  5 }
    };

    // UnidadeMedida: int -> string
    private static readonly Dictionary<int, string> UnidadeParaString = new()
    {
        { 1, "un."   },
        { 2, "%"     },
        { 3, "dias"  },
        { 4, "meses" }
    };

    // UnidadeMedida: string -> int (inverso, para montar o PATCH)
    private static readonly Dictionary<string, int> StringParaUnidade = new()
    {
        { "un.",   1 },
        { "%",     2 },
        { "dias",  3 },
        { "meses", 4 }
    };

    // Dias da semana: int do banco -> índice do bool no DTO
    // O banco usa: 0=Dom, 1=Seg, 2=Ter, 3=Qua, 4=Qui, 5=Sex, 6=Sáb
    // -------------------------------------------------------------------------

    protected override async Task OnInitializedAsync()
    {
        // 1. Descobre o e-mail do usuário logado via JWT
        var tokenRaw = await TokenService.ObterTokenAsync();
        if (!string.IsNullOrWhiteSpace(tokenRaw))
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(tokenRaw.Replace("Bearer ", ""));
                var emailReal = jwtToken.Claims.FirstOrDefault(c => c.Type == "email" || c.Type == "unique_name")?.Value;

                if (!string.IsNullOrEmpty(emailReal))
                    Model.EmailDestinatario = emailReal;
            }
            catch { /* mantém vazio, o GET vai retornar 404 e fica nos defaults */ }
        }

        // 2. Busca os dados reais do banco e mapeia para o Model da tela
        await CarregarDobanco();
    }

    private async Task CarregarDobanco()
    {
        try
        {
            var response = await HttpService.GetAsync($"api/Alerta/email/{Model.EmailDestinatario}");

            if (!response.IsSuccessStatusCode)
            {
                // 404 = usuário ainda não tem registro de alertas (normal no primeiro acesso)
                // Mantém os valores default do Model sem logar como erro
                return;
            }

            var dadosBanco = await response.Content.ReadFromJsonAsync<AlertaHeaderResponseDto>(
                new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (dadosBanco == null) return;

            // Salva o Id do header para usar no PATCH depois
            _headerIdBanco =(int) dadosBanco.Id;

            // Converte o objeto do banco para o formato que a tela entende
            Model = MapearBancoParaDto(dadosBanco);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[Aviso] Falha ao carregar alertas da API: {ex.Message}");
        }
    }

    // -------------------------------------------------------------------------
    // Mapeamento: AlertaHeaderResponseDto (banco) -> AlertConfigurationDto (tela)
    // -------------------------------------------------------------------------
    private AlertConfigurationDto MapearBancoParaDto(AlertaHeaderResponseDto banco)
    {
        // Converte "08:00:00" para TimeSpan
        TimeSpan horario = TimeSpan.Zero;
        if (TimeSpan.TryParse(banco.Horario, out var h))
            horario = h;

        var dto = new AlertConfigurationDto
        {
            EmailDestinatario   = banco.Email,
            EmailAtivo          = (bool)banco.IsEnable,
            HorarioEnvio        = horario,

            // Dias: o banco manda um array de ints (0=Dom ... 6=Sáb)
            FaturamentoDomingo  = banco.Dias.Contains(0),
            FaturamentoSegunda  = banco.Dias.Contains(1),
            FaturamentoTerca    = banco.Dias.Contains(2),
            FaturamentoQuarta   = banco.Dias.Contains(3),
            FaturamentoQuinta   = banco.Dias.Contains(4),
            FaturamentoSexta    = banco.Dias.Contains(5),
            FaturamentoSabado   = banco.Dias.Contains(6),

            Indicators = new()
        };

        foreach (var alerta in banco.Alertas)
        {
            var baseInfo = alerta.AlertaBaseDados;

            // Guarda o Id da linha para usar no PATCH individual de cada alerta
            _alertaConfigIds[(int)alerta.IdAlertaBase] = (int)alerta.Id;

            // Converte Trigger (int) -> operador (string)
            var operadorAtual = TriggerParaOperador.GetValueOrDefault((int)alerta.Trigger, "<=");

            // Lista de operadores permitidos (vem de ValidTrigger da AlertaBase)
            var operadoresPermitidos = baseInfo.ValidTrigger
                .Where(t => TriggerParaOperador.ContainsKey(t))
                .Select(t => TriggerParaOperador[t])
                .ToList();

            // Converte UnidadeMedida (int) -> string
            // Usa o valor do AlertaConfigUser (configuração do usuário),
            // mas cai no padrão da AlertaBase se o do usuário for inválido
            var unidadeInt = UnidadeParaString.ContainsKey((int)alerta.UnidadeMedida)
                ? alerta.UnidadeMedida
                : baseInfo.UnidadeMedida;

            var unidadeAtual = UnidadeParaString.GetValueOrDefault((int)unidadeInt, "un.");

            // Lista de unidades permitidas (vem de UnidadeMedidaValidas da AlertaBase)
            var unidadesPermitidas = baseInfo.UnidadeMedidaValidas
                .Where(u => UnidadeParaString.ContainsKey(u))
                .Select(u => UnidadeParaString[u])
                .ToList();

            dto.Indicators.Add(new AlertConfigurationItemDto
            {
                // Usa o Id numérico do alertabase como Id do item (string, conforme o DTO)
                Id                  = alerta.IdAlertaBase.ToString(),
                Title               = baseInfo.Name,
                Operator            = operadorAtual,
                Unit                = unidadeAtual,
                Value               = (double)alerta.Value,
                IsActive            = (bool)alerta.IsEnable,
                OperadoresPermitidos = operadoresPermitidos,
                UnidadesPermitidas  = unidadesPermitidas
            });
        }

        return dto;
    }

    // -------------------------------------------------------------------------
    // Salvar: PATCH no AlertaHeader + PATCH em cada AlertaConfigUser alterado
    // -------------------------------------------------------------------------
    protected async Task SalvarConfiguracoes()
    {
        if (_headerIdBanco == 0)
        {
            System.Diagnostics.Debug.WriteLine("[Erro] Id do header não encontrado. Carregue os dados antes de salvar.");
            return;
        }

        try
        {
            // --- 1. Atualiza o AlertaHeader (email, horário, dias, ativo) ---
            var diasSelecionados = new List<int>();
            if (Model.FaturamentoDomingo) diasSelecionados.Add(0);
            if (Model.FaturamentoSegunda) diasSelecionados.Add(1);
            if (Model.FaturamentoTerca)   diasSelecionados.Add(2);
            if (Model.FaturamentoQuarta)  diasSelecionados.Add(3);
            if (Model.FaturamentoQuinta)  diasSelecionados.Add(4);
            if (Model.FaturamentoSexta)   diasSelecionados.Add(5);
            if (Model.FaturamentoSabado)  diasSelecionados.Add(6);

            // O PATCH do BaseController espera Dictionary<string, object>
            // As chaves são os nomes das PROPRIEDADES C# do model AlertaHeader
            var patchHeader = new Dictionary<string, object>
            {
                { "Email",        Model.EmailDestinatario },
                { "IsEnable",     Model.EmailAtivo },
                { "Horario",      Model.HorarioEnvio.ToString(@"hh\:mm\:ss") },
                { "Dias",         diasSelecionados.ToArray() },
                { "AtualizadoEm", DateTime.UtcNow }
            };

            var respostaHeader = await HttpService.PatchAsync($"api/Alerta/Update/{_headerIdBanco}", patchHeader);
            if (!respostaHeader.IsSuccessStatusCode)
            {
                System.Diagnostics.Debug.WriteLine($"[Erro] Falha ao salvar header: {respostaHeader.StatusCode}");
                return;
            }

            // --- 2. Atualiza cada AlertaConfigUser individualmente ---
            foreach (var item in Model.Indicators)
            {
                // item.Id é o IdAlertaBase (string), converte de volta para int
                if (!int.TryParse(item.Id, out var idAlertaBase)) continue;

                // Pega o Id da linha na tabela alertaconfigurado
                if (!_alertaConfigIds.TryGetValue(idAlertaBase, out var idLinhaConfig)) continue;

                var triggerInt   = OperadorParaTrigger.GetValueOrDefault(item.Operator, 1);
                var unidadeInt   = StringParaUnidade.GetValueOrDefault(item.Unit, 1);

                // As chaves são os nomes das PROPRIEDADES C# do model AlertaConfigUser
                var patchLinha = new Dictionary<string, object>
                {
                    { "Trigger",      triggerInt  },
                    { "UnidadeMedida", unidadeInt },
                    { "Value",        item.Value  },
                    { "IsEnable",     item.IsActive },
                    { "AtualizadoEm", DateTime.UtcNow.AddHours(-3) }
                };

                var respostaLinha = await HttpService.PatchAsync($"api/Alerta/Update/{_headerIdBanco}/{idLinhaConfig}", patchLinha);

                if (!respostaLinha.IsSuccessStatusCode)
                {
                    System.Diagnostics.Debug.WriteLine($"[Aviso] Falha ao salvar alerta id={idLinhaConfig}: {respostaLinha.StatusCode}");
                }
            }

            System.Diagnostics.Debug.WriteLine("[Sucesso] Configurações de alerta salvas!");
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[Erro] Exceção ao salvar: {ex.Message}");
        }
    }
}