using Sofragrancia.API.Services;
using Sofragrancia.Banco.Models;
using Sofragrancia.Banco.Repositories;

public class FaturaIntegracaoService
{
    private readonly FinanceiroService _financeiroService;
    private readonly FaturaRepository _faturaRepository;

    public FaturaIntegracaoService(
        FinanceiroService financeiroService,
        FaturaRepository faturaRepository)
    {
        _financeiroService = financeiroService;
        _faturaRepository = faturaRepository;
    }

    public async Task SincronizarFaturas()
    {
        var faturasFinanceiro = await _financeiroService.ObterFaturas();

        foreach (var fatura in faturasFinanceiro)
        {
            var entity = new Fatura
            {
                Id = fatura.Id,
                IdPedido = fatura.IdPedido,
                Serial = fatura.NumeroNotaFiscal,
                DocDate = fatura.DataEmissao,
                ValorBruto = (double)fatura.ValorProdutos,
                TotalImposto = (double)(fatura.ValorImposto ?? 0),
                TotalDesconto = (double)(fatura.ValorDesconto ?? 0),
                ValorLiquido = (double)fatura.ValorTotal,
                Status = fatura.Status,
                CriadoEm = fatura.DataCriacao,
                AtualizadoEm = fatura.DataAtualizacao,
                IsEnable = fatura.Ativo
            };

            await _faturaRepository.UpsertModelAsync(entity);
        }
    }
}