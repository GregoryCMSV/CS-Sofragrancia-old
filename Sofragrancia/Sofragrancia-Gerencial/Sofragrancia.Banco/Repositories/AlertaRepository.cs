using Sofragrancia.Banco.Models;
using Sofragrancia.Banco.Models.Alertas;
using Supabase;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace Sofragrancia.Banco.Repositories
{
    public class AlertaRepository : Repository<AlertaHeader>
    {
        public AlertaRepository(Client supabase) : base(supabase)
        {
        }

    }
}
