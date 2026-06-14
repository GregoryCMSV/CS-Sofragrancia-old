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
            
            Dictionary<string, object> dicionario = new();
            if (alerta.Email != null)
            {
                dicionario["email"] = alerta.Email; 
            }
            if (alerta.Horario != null)
            {
                dicionario["horario"] = TimeOnly.Parse(alerta.Horario); 
            }
            if (alerta.Dias != null)
            {
                dicionario["dias"] = alerta.Dias; 
            }
            if (alerta.IsEnable != null)
            {
                dicionario["isEnable"] = alerta.IsEnable; 
            }
            var response = await _repository.UpdateByID(id, dicionario);
            return Ok(response);
        }

        [HttpPatch("Update/{idheader}/{idlinha}")]
        public async Task<IActionResult> UpdateById(int idheader, int idlinha, AlertaConfigUserResponseDto alerta)
        {
            if (!await _repository.HeaderExists(idheader))
                return NotFound("Alerta não encontrado.");
            if (alerta == null)
                return BadRequest("Informe um campo para atualizar.");
            if (!await _repository.LineExists(idlinha))
                return NotFound("Linha não encontrada.");

            Dictionary<string, object> dicionario = new();
            if (alerta.Trigger != null)
            {
                dicionario["trigger"] = alerta.Trigger; 
            }
            if (alerta.UnidadeMedida != null)
            {
                dicionario["unidademedida"] = alerta.UnidadeMedida; 
            }
            if (alerta.Value != null)
            {
                dicionario["value"] = alerta.Value; 
            }
            if (alerta.IsEnable != null)
            {
                dicionario["isEnable"] = alerta.IsEnable; 
            }

            var response = await _repository.UpdateLineByID(idlinha, dicionario);
            return Ok(response);
        }
    }
}
