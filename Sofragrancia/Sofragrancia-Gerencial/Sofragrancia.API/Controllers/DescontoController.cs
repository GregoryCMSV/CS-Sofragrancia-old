using Sofragrancia.Banco.Models;
using Sofragrancia.Banco.Repositories;
using Supabase;

namespace Sofragrancia.API.Controllers
{
    public class DescontoController : BaseController<Desconto, DescontoRepository>
    {
        public DescontoController(Client client) : base(client)
        {
            _repository = new DescontoRepository(client);
        }
    }
}
