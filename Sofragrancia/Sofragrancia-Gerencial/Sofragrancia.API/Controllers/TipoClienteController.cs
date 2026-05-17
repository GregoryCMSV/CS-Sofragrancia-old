using Sofragrancia.Banco.Models;
using Sofragrancia.Banco.Repositories;
using Supabase;

namespace Sofragrancia.API.Controllers
{
    public class TipoClienteController : BaseController<TipoCliente, TipoClienteRepository>
    {
        public TipoClienteController(Client client) : base(client)
        {
            _repository = new(client);
        }
    }
}
