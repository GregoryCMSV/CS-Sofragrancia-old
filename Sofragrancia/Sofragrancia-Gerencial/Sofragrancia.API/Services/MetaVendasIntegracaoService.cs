using Sofragrancia.Banco.Models;
using Sofragrancia.Banco.Repositories;
using Sofragrancia.API.DTOs;

namespace Sofragrancia.API.Services
{
    public class MetaVendasIntegracaoService
    {
        private readonly FinanceiroService _financeiroService;
        private readonly MetaVendasRepository _metaRepository;

        public MetaVendasIntegracaoService(
            FinanceiroService financeiroService,
            MetaVendasRepository metaRepository)
        {
            _financeiroService = financeiroService;
            _metaRepository = metaRepository;
        }

        public async Task SincronizarMetasVendas()
        {
            var metas = await _financeiroService.ObterMetasVendas();

            if (metas == null || metas.Count == 0)
                return;

            foreach (var meta in metas)
            {
                var entidade = Mapear(meta);

                await _metaRepository.InsertModelAsync(entidade);
            }
        }

        private MetaVendas Mapear(MetaVendaFinanceiroDto dto)
        {
            return new MetaVendas
            {
                Id = dto.Id,
                IdVendedor = dto.IdVendedor,
                IdProduto = dto.IdProduto,
                Ano = dto.Ano,
                Mes = int.TryParse(dto.Mes, out var mes) ? mes : 0,

                QtdDemanda = dto.QuantidadeMeta,
                QtdRealizada = dto.QuantidadeRealizada,

                ValorMeta = (double)dto.ValorMeta,
                ValorRealizado = (double)dto.ValorRealizado,

                CriadoEm = dto.DataCriacao,
                AtualizadoEm = dto.DataAtualizacao,
                IsEnable = dto.Ativo
            };
        }
    }
}