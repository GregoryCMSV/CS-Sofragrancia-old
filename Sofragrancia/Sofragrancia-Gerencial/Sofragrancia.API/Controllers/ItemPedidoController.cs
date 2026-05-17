using Sofragrancia.Banco.Models;
using Sofragrancia.Banco.Repositories;
using Supabase;

namespace Sofragrancia.API.Controllers
{
    public class ItemPedidoController : BaseController<ItemPedido, ItemPedidoRepository>
    {
        public ItemPedidoController(Client client) : base(client)
        {
            _repository = new(client);
        }
    }
}
