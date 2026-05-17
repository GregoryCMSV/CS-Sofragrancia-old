using Sofragrancia.Banco.Models;
using Sofragrancia.Banco.Repositories;
using Supabase;

namespace Sofragrancia.API.Controllers
{
    public class VendedorController : BaseController<Vendedor, VendedorRepository>
    {
        public VendedorController(Client client) : base(client)
        {
            _repository = new(client);
        }
    }
}
