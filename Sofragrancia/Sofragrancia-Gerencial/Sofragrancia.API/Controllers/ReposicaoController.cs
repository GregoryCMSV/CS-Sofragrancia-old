using Microsoft.AspNetCore.Mvc;
using Sofragrancia.API.Services;

namespace Sofragrancia.API.Controllers
{
    [ApiController]
    [Route("api/reposicao")]
    public class ReposicaoController : ControllerBase
    {
        private readonly ReposicaoService _reposicaoService;

        public ReposicaoController(ReposicaoService reposicaoService)
        {
            _reposicaoService = reposicaoService;
        }


        [HttpGet("reposicoes")]
        public async Task<IActionResult> GetReposicoes()
        {
            var reposicoes = await _reposicaoService.ObterReposicoes();
            return Ok(reposicoes);
        }
    }

}