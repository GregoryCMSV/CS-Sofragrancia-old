using Sofragrancia.Banco.Models;
using Sofragrancia.Banco.Repositories;
using Supabase;

namespace Sofragrancia.API.Controllers
{
    public class FornecedorController : BaseController<Fornecedor, FornecedorRepository>
    {
        public FornecedorController(Client client) : base(client)
        {
            _repository = new FornecedorRepository(client);
        }
    }
}
