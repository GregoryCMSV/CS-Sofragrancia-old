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
            
            /*var metadados = alerta.GetType()
            .GetProperties()
            .Where(p => p.GetValue(alerta) != null && p.GetValue(alerta) != default)
            .ToDictionary(
                p => p.Name,
                p => p.GetValue(alerta) ?? string.Empty
            );
            */
            Dictionary<string, object> dicionario = new();
            if (alerta.Email != null)
            {
                dicionario["email"] = alerta.Email; 
            }
            if (alerta.Horario != null)
            {
                dicionario["horario"] = alerta.Horario; 
            }
            if (alerta.Dias != null)
            {
                dicionario["dias"] = alerta.Dias; 
            }
            if (alerta.IsEnable != null)
            {
                dicionario["isEnable"] = alerta.IsEnable; 
            }
            /*
            if (alerta.Alertas != null)
            {
                //dicionario["alertas"] = new Dictionary<string, object>
                dicionario["alertas"] = new List<AlertaConfigUserResponseDto>();
            }
            */

            var response = await _repository.PatchByID(id, dicionario);
            return Ok(response);
        }
    }
}
