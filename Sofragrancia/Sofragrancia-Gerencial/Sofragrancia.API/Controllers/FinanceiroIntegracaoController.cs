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

        public FinanceiroIntegracaoController(FinanceiroService service, ProdutoIntegracaoService produtoIntegracaoService)
        {
            _service = service;
            _produtoIntegracaoService = produtoIntegracaoService;
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
    }

}