using Sofragrancia.Banco.Models;
using Supabase;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sofragrancia.Banco.Repositories
{
    public class ItemPedidoRepository : Repository<ItemPedido>
    {
        public ItemPedidoRepository(Client supabase) : base(supabase)
        {
        }
    }
}
