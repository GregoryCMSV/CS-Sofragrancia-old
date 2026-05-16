using Sofragrancia.Banco.Models;
using Supabase;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sofragrancia.Banco.Repositories
{
    public class MetaVendasRepository : Repository<MetaVendas>
    {
        public MetaVendasRepository(Client supabase) : base(supabase)
        {
        }
    }
}
