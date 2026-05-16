using Sofragrancia.Banco.Models;
using Supabase;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sofragrancia.Banco.Repositories
{
    public class FaturaRepository : Repository<Fatura>
    {
        public FaturaRepository(Client supabase) : base(supabase)
        {
        }
    }
}
