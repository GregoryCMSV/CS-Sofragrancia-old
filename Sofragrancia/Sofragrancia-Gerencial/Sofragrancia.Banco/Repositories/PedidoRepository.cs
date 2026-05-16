using Sofragrancia.Banco.Models;
using Supabase;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sofragrancia.Banco.Repositories
{
    public class PedidoRepository : Repository<Pedido>
    {
        public PedidoRepository(Client supabase) : base(supabase)
        {
        }
    }
}
