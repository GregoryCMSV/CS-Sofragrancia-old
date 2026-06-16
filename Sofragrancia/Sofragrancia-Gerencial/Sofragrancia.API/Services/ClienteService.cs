using Sofragrancia.Banco.Models;
using Sofragrancia.Banco.Repositories;
using Sofragrancia.API.DTOs;

namespace Sofragrancia.API.Services
{
    public class ClienteService
    {
        private readonly FinanceiroService _financeiroService;
        private readonly ClienteRepository _clienteRepository;

        public ClienteService(
            FinanceiroService financeiroService,
            ClienteRepository clienteRepository)
        {
            _financeiroService = financeiroService;
            _clienteRepository = clienteRepository;
        }
        internal async Task SincronizarCliente()
        {
            var clientes = await _financeiroService.ObterClientes();

            if (clientes == null || clientes.Count() == 0)
                return;

            foreach (var cliente in clientes)
            {
                var entidade = Mapear(cliente);

                await _clienteRepository.UpsertModelAsync(entidade);
            }
        }

        private Cliente Mapear(ClienteDto dto)
        {
            return new Cliente
            {
                Id = dto.Id,
                Name = dto.RazaoSocial,
                Cnpj = dto.Cnpj,
                telefone = dto.Telefone ?? string.Empty,
                Email = dto.Email ?? string.Empty,
                Endereco = dto.Endereco ?? string.Empty,
                Cidade = dto.Cidade ?? string.Empty,
                Estado = dto.Estado ?? string.Empty,
                
                // Conversão de decimal? para double (exigido pela entidade Cliente)
                Limitecredito = dto.LimiteCredito.HasValue ? (double)dto.LimiteCredito.Value : 0.0,
                
                CriadoEm = dto.DataCriacao,
                AtualizadoEm = dto.DataAtualizacao,
                IsEnable = dto.Ativo,
                
                // Convertendo o int do DTO para string, já que a entidade espera string nesta propriedade
                Codigo = dto.CodCliente.ToString(),
                
                // Propriedade existente na entidade que não veio no DTO (definindo um valor padrão ou 0)
                TipoCliente = 2 
            };
        }
    }
}