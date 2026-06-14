using Microsoft.AspNetCore.Mvc;
using Sofragrancia.Banco.Models.Alertas;
using Sofragrancia.Banco.Repositories;
using Sofragrancia.Shared.Dtos;
using Supabase;

namespace Sofragrancia.API.Controllers
{
    public class AlertaController : BaseController<AlertaHeader, AlertaRepository>
    {
        public AlertaController(Client client) : base(client)
        {
            _repository = new(client);
        }

        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            var dado = await _repository.GetByEmailAsync(email);

            if (dado == null)
            {
                return NotFound(new { Mensagem = "Alerta não encontrado para o e-mail informado." });
            }

            return Ok(dado);
        }
        [HttpPatch("Update/{id}")]
        public async Task<IActionResult> UpdateById(int id, AlertaHeaderResponseDto alerta)
        {
            if (!await _repository.HeaderExists(id))
                return NotFound("Alerta não encontrado.");
            if (alerta == null)
                return BadRequest("Informe um campo para atualizar.");
            
            var metadados = alerta.GetType()
            .GetProperties()
            .Where(p => p.GetValue(alerta) != null)
            .ToDictionary(
                p => p.Name,
                p => p.GetValue(alerta) ?? string.Empty
            );


            return Ok();
        }
    }
}
