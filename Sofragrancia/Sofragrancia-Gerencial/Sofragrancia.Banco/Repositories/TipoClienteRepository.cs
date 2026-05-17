using Sofragrancia.Banco.Models;
using Supabase;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sofragrancia.Banco.Repositories
{
    public class TipoClienteRepository : Repository<TipoCliente>
    {
        public TipoClienteRepository(Client supabase) : base(supabase)
        {
        }
    }
}
