using Microsoft.AspNetCore.Mvc;
using Sofragrancia.API.Services;

namespace Sofragrancia.API.Controllers
{
    [ApiController]
    [Route("api/financeiro")]
    public class FinanceiroIntegracaoController : ControllerBase
    {
        private readonly FinanceiroService _service;
        private readonly ProdutoIntegracaoService _produtoIntegracaoService;
        private readonly PedidoIntegracaoService _pedidoIntegracaoService;
        private readonly FaturaIntegracaoService _faturaIntegracaoService;

        private readonly ItemPedidoIntegracaoService _itemPedidoIntegracaoService;
        private readonly DescontoIntegracaoService _descontoIntegracaoService;
        private readonly MetaVendasIntegracaoService _metaVendasIntegracaoService;
        private readonly ClienteService _clienteService;
        public FinanceiroIntegracaoController(FinanceiroService service, ProdutoIntegracaoService produtoIntegracaoService,
        PedidoIntegracaoService pedidoIntegracaoService, ItemPedidoIntegracaoService itemPedidoIntegracaoService, FaturaIntegracaoService faturaIntegracaoService,
        DescontoIntegracaoService descontoIntegracaoService, MetaVendasIntegracaoService metaVendasIntegracaoService, ClienteService clienteService)
        {
            _service = service;
            _produtoIntegracaoService = produtoIntegracaoService;
            _pedidoIntegracaoService = pedidoIntegracaoService;
            _itemPedidoIntegracaoService = itemPedidoIntegracaoService;
            _faturaIntegracaoService = faturaIntegracaoService;
            _descontoIntegracaoService = descontoIntegracaoService;
            _metaVendasIntegracaoService = metaVendasIntegracaoService;
            _clienteService = clienteService;
        }


        [HttpGet("produtos")]
        public async Task<IActionResult> GetProdutos()
        {
            var produtos = await _service.ObterProdutos();

            return Ok(produtos);
        }

        
        [HttpGet("pedidos")]
        public async Task<IActionResult> GetPedidos()
        {
            var pedidos = await _service.ObterPedidos();

            return Ok(pedidos);
        }

        
        [HttpGet("descontos")]
        public async Task<IActionResult> GetDescontos()
        {
            var descontos = await _service.ObterDescontos();

            return Ok(descontos);
        }

        [HttpGet("faturas")]
        public async Task<IActionResult> GetFaturas()
        {
            var faturas = await _service.ObterFaturas();

            return Ok(faturas);
        }

        [HttpGet("fornecedores")]
        public async Task<IActionResult> GetFornecedores()
        {
            var fornecedores = await _service.ObterFornecedores();

            return Ok(fornecedores);
        }

        [HttpGet("metas-vendas")]
        public async Task<IActionResult> GetMetasVendas()
        {
            var metas = await _service.ObterMetasVendas();

            return Ok(metas);
        }

        [HttpGet("itens-pedido")]
        public async Task<IActionResult> GetItensPedido()
        {
            var itens = await _service.ObterItensPedido();

            return Ok(itens);
        }
    
        [HttpPost("sincronizar-produtos")]
        public async Task<IActionResult> SincronizarProdutos()
        {
            await _produtoIntegracaoService.SincronizarProdutos();

            return Ok("Produtos sincronizados");
        }
        
        [HttpPost("sincronizar-pedidos")]
        public async Task<IActionResult> SincronizarPedidos()
        {
            await _pedidoIntegracaoService.SincronizarPedidos();

            return Ok("Pedidos sincronizados");
        }

        [HttpPost("sincronizar-itens-pedido")]
        public async Task<IActionResult> SincronizarItensPedido()
        {
            await _itemPedidoIntegracaoService.SincronizarItensPedido();
            return Ok("Itens de pedido sincronizados");
        }

        [HttpPost("sincronizar-faturas")]
        public async Task<IActionResult> SincronizarFaturas()
        {
            await _faturaIntegracaoService.SincronizarFaturas();
            return Ok("Faturas sincronizadas");
        }

        [HttpPost("sincronizar-descontos")]
        public async Task<IActionResult> SincronizarDescontos()
        {
            await _descontoIntegracaoService.SincronizarDescontos();
            return Ok("Descontos sincronizados");
        }

        [HttpPost("sincronizar-metas-vendas")]
        public async Task<IActionResult> SincronizarMetasVendas()
        {
            await _metaVendasIntegracaoService.SincronizarMetasVendas();
            return Ok("Metas de vendas sincronizadas");
        }

        [HttpPost("sincronizar-cliente")]
        public async Task<IActionResult> SincronizarCliente()
        {
            await _clienteService.SincronizarCliente();
            return Ok("Cliente sincronizado");
        }
    }

}