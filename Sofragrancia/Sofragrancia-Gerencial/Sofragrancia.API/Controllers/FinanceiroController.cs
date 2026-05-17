using Sofragrancia.Banco.Models;
using Sofragrancia.Banco.Repositories;
using Supabase;

namespace Sofragrancia.API.Controllers
{
    public class FinanceiroController : BaseController<Financeiro, FinanceiroRepository>
    {
        public FinanceiroController(Client client) : base(client)
        {
            _repository = new(client);
        }
    }
}
