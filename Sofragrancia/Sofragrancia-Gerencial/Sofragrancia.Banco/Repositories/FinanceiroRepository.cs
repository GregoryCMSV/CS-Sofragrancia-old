using Sofragrancia.Banco.Models;
using Supabase;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sofragrancia.Banco.Repositories
{
    public class FinanceiroRepository : Repository<Financeiro>
    {
        public FinanceiroRepository(Client supabase) : base(supabase)
        {
        }
    }
}
