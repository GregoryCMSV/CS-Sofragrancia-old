using Sofragrancia.Banco.Models.Alertas;
using Sofragrancia.Banco.Repositories;
using Supabase;

namespace Sofragrancia.API.Controllers
{
    public class AlertaController : BaseController<AlertaBase, AlertaRepository>
    {
        public AlertaController(Client client) : base(client)
        {
            _repository = new(client);
        }
    }
}
