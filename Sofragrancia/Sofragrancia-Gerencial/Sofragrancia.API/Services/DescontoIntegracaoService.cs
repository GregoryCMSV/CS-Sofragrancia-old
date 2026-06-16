using Sofragrancia.API.Services;
using Sofragrancia.Banco.Models;
using Sofragrancia.Banco.Repositories;

public class DescontoIntegracaoService
{
    private readonly FinanceiroService _financeiroService;
    private readonly DescontoRepository _descontoRepository;

    public DescontoIntegracaoService(
        FinanceiroService financeiroService,
        DescontoRepository descontoRepository)
    {
        _financeiroService = financeiroService;
        _descontoRepository = descontoRepository;
    }

    public async Task SincronizarDescontos()
    {
        var descontosFinanceiro = await _financeiroService.ObterDescontos();

        foreach (var desconto in descontosFinanceiro)
        {
            var entity = new Desconto
            {
                Id = desconto.Id,
                Descricao = desconto.Descricao,
                ValorMin = (double)desconto.ValorMinimo,
                ValorMax = (double)desconto.ValorMaximo,
                Percentual = (double)desconto.Percentual,
                IdCliente = desconto.IdCliente,
                IdProduto = desconto.IdProduto,
                IdVendedor = desconto.IdVendedor,
                CriadoEm = desconto.DataCriacao,
                AtualizadoEm = desconto.DataAtualizacao,
                IsEnable = desconto.Ativo
            };

            await _descontoRepository.UpsertModelAsync(entity);
        }
    }
}