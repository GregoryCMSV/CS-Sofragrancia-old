using Sofragrancia.API.Services;
using Sofragrancia.Banco.Models;
using Sofragrancia.Banco.Repositories;

public class ItemPedidoIntegracaoService
{
    private readonly FinanceiroService _financeiroService;
    private readonly ItemPedidoRepository _itemPedidoRepository;

    public ItemPedidoIntegracaoService(
        FinanceiroService financeiroService,
        ItemPedidoRepository itemPedidoRepository)
    {
        _financeiroService = financeiroService;
        _itemPedidoRepository = itemPedidoRepository;
    }

    public async Task SincronizarItensPedido()
    {
        var itensFinanceiro = await _financeiroService.ObterItensPedido();

        foreach (var item in itensFinanceiro)
        {
            var entity = new ItemPedido
            {
                Id = item.Id,
                IdPedido = item.IdPedido,
                IdProduto = item.IdProduto,
                Quantidade = item.Quantidade,
                PrecoUnitario = item.PrecoUnitario,
                Desconto = item.DescontoUnitario,
                Subtotal = item.Subtotal,
                CriadoEm = item.DataCriacao,
                AtualizadoEm = item.DataAtualizacao,
                IsEnable = item.Ativo
            };

            await _itemPedidoRepository.InsertModelAsync(entity);
        }
    }
}