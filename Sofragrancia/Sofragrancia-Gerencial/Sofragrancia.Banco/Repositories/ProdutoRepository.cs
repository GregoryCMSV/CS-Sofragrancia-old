using Sofragrancia.Banco.Models;
using Supabase;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sofragrancia.Banco.Repositories
{
    public class ProdutoRepository : Repository<Produto>
    {
        public ProdutoRepository(Client supabase) : base(supabase)
        {
        }
    }
}
