using Sofragrancia.Banco.Models;
using Supabase;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sofragrancia.Banco.Repositories
{
    public class ClienteRepository : Repository<Cliente>
    {
        public ClienteRepository(Client supabase) : base(supabase)
        {
        }
    }
}
