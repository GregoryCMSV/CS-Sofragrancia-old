using Sofragrancia.Banco.Models;
using Sofragrancia.Banco.Repositories;
using Supabase;

namespace Sofragrancia.API.Controllers
{
    public class FaturaController : BaseController<Fatura, FaturaRepository>
    {
        public FaturaController(Client client) : base(client)
        {
            _repository = new(client);
        }
    }
}
