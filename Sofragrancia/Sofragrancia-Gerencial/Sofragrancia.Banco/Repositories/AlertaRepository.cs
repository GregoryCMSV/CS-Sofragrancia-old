using Sofragrancia.Banco.Models.Alertas;
using Supabase;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sofragrancia.Banco.Repositories
{
    public class AlertaRepository : Repository<AlertaBase>
    {
        public AlertaRepository(Client supabase) : base(supabase)
        {
        }
    }
}
