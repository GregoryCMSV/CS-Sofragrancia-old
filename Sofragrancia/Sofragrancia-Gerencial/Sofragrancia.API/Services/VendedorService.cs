using Sofragrancia.Banco.Models;
using Sofragrancia.Banco.Repositories;
using Sofragrancia.API.DTOs;

namespace Sofragrancia.API.Services
{
    public class VendedorService
    {
        private readonly FinanceiroService _financeiroService;
        private readonly VendedorRepository _vendedorRepository;

        public VendedorService(
            FinanceiroService financeiroService,
            VendedorRepository vendedorRepository)
        {
            _financeiroService = financeiroService;
            _vendedorRepository = vendedorRepository;
        }
        internal async Task SincronizarVendedor()
        {
            var vendedores = await _financeiroService.ObterVendedores();

            if (vendedores == null || vendedores.Count() == 0)
                return;

            foreach (var vendedor in vendedores)
            {
                var entidade = Mapear(vendedor);

                await _vendedorRepository.UpsertModelAsync(entidade);
            }
        }

        private Vendedor Mapear(VendedorDto dto)
        {
            return new Vendedor
            {
                Id = dto.Id,
                
                // Convertendo o int do DTO para string na entidade
                Codigo = dto.CodVendedor.ToString(),
                
                Name = dto.Nome,
                Cpf = dto.Cpf,
                
                // Tratando campos de texto anuláveis do DTO
                Tel = dto.Telefone ?? string.Empty,
                Email = dto.Email,
                Regiao = dto.Regiao ?? string.Empty,
                
                // Tratando a data anulável (se for nula, define uma data padrão padrão)
                Admissao = dto.DataAdmissao ?? DateTime.MinValue,
                
                CriadoEm = dto.DataCriacao,
                AtualizadoEm = dto.DataAtualizacao,
                IsEnable = dto.Ativo
            };
        }
    }
}