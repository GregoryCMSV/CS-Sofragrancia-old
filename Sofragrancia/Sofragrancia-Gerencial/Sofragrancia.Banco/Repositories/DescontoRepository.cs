using Sofragrancia.Banco.Models;
using Supabase;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sofragrancia.Banco.Repositories
{
    public class DescontoRepository : Repository<Desconto>
    {
        public DescontoRepository(Client supabase) : base(supabase)
        {
        }
    }
}
