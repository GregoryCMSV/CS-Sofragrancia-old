using Microsoft.AspNetCore.Mvc;
using Sofragrancia.Banco.Models.Alertas;
using Sofragrancia.Banco.Repositories;
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

    }
}
