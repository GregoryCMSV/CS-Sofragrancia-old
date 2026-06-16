using Sofragrancia.Banco.Models;
using Sofragrancia.Banco.Repositories;
using Sofragrancia.API.DTOs;

namespace Sofragrancia.API.Services
{
    public class FornecedorService
    {
        private readonly FinanceiroService _financeiroService;
        private readonly FornecedorRepository _fornecedorRepository;

        public FornecedorService(
            FinanceiroService financeiroService,
            FornecedorRepository fornecedorRepository)
        {
            _financeiroService = financeiroService;
            _fornecedorRepository = fornecedorRepository;
        }
        internal async Task SincronizarFornecedor()
        {
            var fornecedores = await _financeiroService.ObterFornecedores();

            if (fornecedores == null || fornecedores.Count() == 0)
                return;

            foreach (var fornecedor in fornecedores)
            {
                var entidade = Mapear(fornecedor);

                await _fornecedorRepository.UpsertModelAsync(entidade);
            }
        }

        private Fornecedor Mapear(FornecedorDto dto)
        {
            return new Fornecedor
            {
                Id = dto.Id,
                Name = dto.Nome,
                Cnpj = dto.CnpjCpf,
                Tel = dto.Telefone,
                Email = dto.Email,
                Endereco = dto.Endereco,
                Representante = dto.Representante,
                CriadoEm = dto.DataCriacao,
                AtualizadoEm = dto.DataAtualizacao,
                IsEnable = dto.Ativo
            };
        }
    }
}