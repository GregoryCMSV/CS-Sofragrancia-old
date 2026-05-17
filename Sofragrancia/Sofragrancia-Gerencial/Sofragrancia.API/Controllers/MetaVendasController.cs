using Sofragrancia.Banco.Models;
using Sofragrancia.Banco.Repositories;
using Supabase;

namespace Sofragrancia.API.Controllers
{
    public class MetaVendasController : BaseController<MetaVendas, MetaVendasRepository>
    {
        public MetaVendasController(Client client) : base(client)
        {
            _repository = new(client);
        }
    }
}
