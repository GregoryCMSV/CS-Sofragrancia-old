using Sofragrancia.Banco.Models;
using Sofragrancia.Banco.Repositories;
using Sofragrancia.API.DTOs;

namespace Sofragrancia.API.Services
{
    public class ProdutoService
    {
        private readonly FinanceiroService _financeiroService;
        private readonly ProdutoRepository _produtoRepository;

        public ProdutoService(
            FinanceiroService financeiroService,
            ProdutoRepository produtoRepository)
        {
            _financeiroService = financeiroService;
            _produtoRepository = produtoRepository;
        }
        internal async Task SincronizarProduto()
        {
            var produtos = await _financeiroService.ObterProdutos();

            if (produtos == null || produtos.Count() == 0)
                return;

            foreach (var produto in produtos)
            {
                var entidade = Mapear(produto);

                await _produtoRepository.UpsertModelAsync(entidade);
            }
        }

        private Produto Mapear(ProdutoDto dto)
        {
            return new Produto
            {
                Id = dto.Id,
                IdFornecedor = dto.IdFornecedor,
                Descricao = dto.Descricao,
                
                // Tratando as strings nulas do DTO antes de passar para a Entidade
                Marca = dto.Marca ?? string.Empty,
                Categoria = dto.Categoria ?? string.Empty,
                
                unidade = dto.Unidade,
                PrecoCusto = dto.PrecoCusto,
                PrecoVenda = dto.PrecoVenda,
                EstoqueAtual = dto.EstoqueAtual,
                EstoqueMinimo = dto.EstoqueMinimo,
                
                CriadoEm = dto.CriadoEm,
                AtualizadoEm = dto.AtualizadoEm,
                IsEnable = dto.IsEnable
            };
        }
    }
}