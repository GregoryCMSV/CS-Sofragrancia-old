using Sofragrancia.Banco.Models;
using Supabase;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sofragrancia.Banco.Repositories
{
    public class VendedorRepository : Repository<Vendedor>
    {
        public VendedorRepository(Client supabase) : base(supabase)
        {
        }
    }
}
