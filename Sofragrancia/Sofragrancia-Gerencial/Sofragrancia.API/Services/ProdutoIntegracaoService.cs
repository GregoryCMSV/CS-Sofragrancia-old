using Sofragrancia.API.Services;
using Sofragrancia.Banco.Models;
using Sofragrancia.Banco.Repositories;

public class ProdutoIntegracaoService
{
    private readonly FinanceiroService _financeiroService;
    private readonly ProdutoRepository _produtoRepository;


    public ProdutoIntegracaoService(
        FinanceiroService financeiroService,
        ProdutoRepository produtoRepository)
    {
        _financeiroService = financeiroService;
        _produtoRepository = produtoRepository;
    }


    public async Task SincronizarProdutos()
    {
        var produtosFinanceiro = await _financeiroService.ObterProdutos();


        foreach(var produtoFinanceiro in produtosFinanceiro)
        {
            var produto = new Produto
            {
                IdFornecedor = produtoFinanceiro.IdFornecedor,
                Descricao = produtoFinanceiro.Descricao,
                Marca = produtoFinanceiro.Marca,
                PrecoCusto = produtoFinanceiro.PrecoCusto,
                PrecoVenda = produtoFinanceiro.PrecoVenda,
                EstoqueAtual = produtoFinanceiro.EstoqueAtual,
                EstoqueMinimo = produtoFinanceiro.EstoqueMinimo,
                Categoria = produtoFinanceiro.Categoria,
                CriadoEm = produtoFinanceiro.CriadoEm,
                AtualizadoEm = produtoFinanceiro.AtualizadoEm,
                IsEnable = produtoFinanceiro.IsEnable
            };

            await _produtoRepository.UpsertModelAsync(produto);
        }
    }
}