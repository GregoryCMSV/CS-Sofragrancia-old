using Microsoft.AspNetCore.Mvc;
using Sofragrancia.Banco.Models;
using Sofragrancia.Banco.Repositories;
using Supabase;

namespace Sofragrancia.API.Controllers
{
    public class PedidoController : BaseController<Pedido, PedidoRepository>
    {
        public PedidoController(Client client) : base(client)
        {
            _repository = new(client);
        }
    }
}
