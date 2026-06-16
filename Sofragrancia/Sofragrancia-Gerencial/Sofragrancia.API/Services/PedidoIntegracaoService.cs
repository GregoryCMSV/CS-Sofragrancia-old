using Sofragrancia.API.Services;
using Sofragrancia.Banco.Models;
using Sofragrancia.Banco.Repositories;

public class PedidoIntegracaoService
{
    private readonly FinanceiroService _financeiroService;
    private readonly PedidoRepository _pedidoRepository;


    public PedidoIntegracaoService(
        FinanceiroService financeiroService,
        PedidoRepository pedidoRepository)
    {
        _financeiroService = financeiroService;
        _pedidoRepository = pedidoRepository;
    }


    public async Task SincronizarPedidos()
    {
        var pedidosFinanceiro = await _financeiroService.ObterPedidos();


        foreach(var pedidoFinanceiro in pedidosFinanceiro)
        {
            var pedido = new Pedido
            {
                Id = pedidoFinanceiro.Id,

                CodPedido = pedidoFinanceiro.NumeroPedido,

                DocDate = pedidoFinanceiro.DataPedido,

                DocHour = pedidoFinanceiro.HoraPedido,

                IdCliente = pedidoFinanceiro.IdCliente,

                IdVendedor = pedidoFinanceiro.IdVendedor,

                Status = pedidoFinanceiro.Status,

                Observacao = pedidoFinanceiro.Observacao,

                ValorBruto = pedidoFinanceiro.ValorBruto,

                ValorDesconto = pedidoFinanceiro.ValorDesconto,

                ValorLiquido = pedidoFinanceiro.ValorLiquido,

                CriadoEm = pedidoFinanceiro.CriadoEm,

                AtualizadoEm = pedidoFinanceiro.AtualizadoEm,

                IsEnable = pedidoFinanceiro.IsEnable
            };


            await _pedidoRepository.UpsertModelAsync(pedido);
        }
    }
}