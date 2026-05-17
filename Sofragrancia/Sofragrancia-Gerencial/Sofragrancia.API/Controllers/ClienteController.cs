using Sofragrancia.Banco.Models;
using Sofragrancia.Banco.Repositories;
using Supabase;

namespace Sofragrancia.API.Controllers
{
    public class ClienteController : BaseController<Cliente, ClienteRepository>
    {
        public ClienteController(Client client) : base(client)
        {
            _repository = new ClienteRepository(client);
        }
    }
}
