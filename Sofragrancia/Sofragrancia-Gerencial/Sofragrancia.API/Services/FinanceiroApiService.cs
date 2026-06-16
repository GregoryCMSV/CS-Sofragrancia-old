using System.Net.Http.Json;
using Sofragrancia.API.DTOs;

namespace Sofragrancia.API.Services
{
    public class FinanceiroService
    {
        private readonly HttpClient _httpClient;

        public FinanceiroService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<List<ProdutoDto>> ObterProdutos()
        {
            var response = await _httpClient.GetAsync("/rest/v1/produto");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<List<ProdutoDto>>();
        }

        public async Task<List<PedidoFinanceiroDto>> ObterPedidos()
        {
            var response = await _httpClient.GetAsync("/rest/v1/pedido");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<List<PedidoFinanceiroDto>>() ?? new List<PedidoFinanceiroDto>();
        }

        public async Task<List<DescontoFinanceiroDto>> ObterDescontos()
        {
            var response = await _httpClient.GetAsync("/rest/v1/desconto");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<List<DescontoFinanceiroDto>>() ?? new List<DescontoFinanceiroDto>();
        }

        public async Task<List<FaturaFinanceiroDto>> ObterFaturas()
        {
            var response = await _httpClient.GetAsync("/rest/v1/fatura");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<List<FaturaFinanceiroDto>>() ?? new List<FaturaFinanceiroDto>();
        }

        public async Task<List<FornecedorDto>> ObterFornecedores()
        {
            var response = await _httpClient.GetAsync("/rest/v1/fornecedor");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<List<FornecedorDto>>() ?? new List<FornecedorDto>();
        }

        public async Task<List<MetaVendaFinanceiroDto>> ObterMetasVendas()
        {
            var response = await _httpClient.GetAsync("/rest/v1/meta_vendas");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<List<MetaVendaFinanceiroDto>>() ?? new List<MetaVendaFinanceiroDto>();
        }

        public async Task<List<ItemPedidoFinanceiroDto>> ObterItensPedido()
        {
            var response = await _httpClient.GetAsync("/rest/v1/item_pedido");

            response.EnsureSuccessStatusCode();
            var Content = await response.Content.ReadAsStringAsync();
            return await response.Content.ReadFromJsonAsync<List<ItemPedidoFinanceiroDto>>() ?? new List<ItemPedidoFinanceiroDto>();
        }

        internal async Task<IEnumerable<ClienteDto>> ObterClientes()
        {
            var response = await _httpClient.GetAsync("/rest/v1/cliente");

            response.EnsureSuccessStatusCode();
            var Content = await response.Content.ReadAsStringAsync();
            return await response.Content.ReadFromJsonAsync<List<ClienteDto>>() ?? new List<ClienteDto>();
        }

        internal async Task<IEnumerable<VendedorDto>> ObterVendedores()
        {
            var response = await _httpClient.GetAsync("/rest/v1/vendedor");

            response.EnsureSuccessStatusCode();
            var Content = await response.Content.ReadAsStringAsync();
            return await response.Content.ReadFromJsonAsync<List<VendedorDto>>() ?? new List<VendedorDto>();
        }
    }
}