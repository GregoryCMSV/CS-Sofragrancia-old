using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sofragrancia.Banco.Interfaces;
using Sofragrancia.Banco.Repositories;
using Supabase;
using Supabase.Postgrest.Models;

namespace Sofragrancia.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public abstract class BaseController<TEntity, TRepository> : ControllerBase
        where TEntity : BaseModel, IEntidadeBase, new()
        where TRepository : Repository<TEntity>
    {
        protected TRepository _repository;

        protected BaseController(Client client)
        {
        }

        [HttpGet]
        public virtual async Task<IActionResult> GetAll()
        {
            var dados = await _repository.GetAllModelAsync();

            return Ok(dados.OrderBy(d => d.Id));
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetById(int id)
        {
            var dado = await _repository.GetModelByIDAsync(id);
            if (dado == null)
            {
                return NotFound(new { Mensagem = "Registro não encontrado." });
            }
            return Ok(dado);
        }

        [HttpPatch("{id}")]
        public virtual async Task<IActionResult> Patch(int id, [FromBody] Dictionary<string, object> atualizacoes)
        {
            if (atualizacoes == null || !atualizacoes.Any())
            {
                return BadRequest(new { Mensagem = "Nenhum campo foi enviado para atualização." });
            }

            var atualizado = await _repository.UpdateByID(id, atualizacoes);
            if (atualizado == null)
            {
                return NotFound(new { Mensagem = "Registro não encontrado." });
            }

            return Ok(atualizado);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Create([FromBody] TEntity entity)
        {
            var novoItem = await _repository.InsertModelAsync(entity);
            return Created("", new { Mensagem = "Cadastro realizado com sucesso!", novoItem });
        }
    }
}
