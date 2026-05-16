using Sofragrancia.Banco.Models;
using Supabase;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sofragrancia.Banco.Repositories
{
    public class FornecedorRepository : Repository<Fornecedor>
    {
        public FornecedorRepository(Client supabase) : base(supabase)
        {
        }
    }
}
