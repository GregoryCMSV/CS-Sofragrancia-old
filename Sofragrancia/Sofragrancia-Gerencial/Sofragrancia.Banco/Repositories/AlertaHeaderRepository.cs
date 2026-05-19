using Sofragrancia.Banco.Models.Alertas;
using Supabase;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sofragrancia.Banco.Repositories
{
    public class AlertaBaseRepository : Repository<AlertaBase>
    {
        public AlertaBaseRepository(Client supabase) : base(supabase)
        {
        }
    }
    public class AlertaConfigUserRepository : Repository<AlertaConfigUser>
    {
        public AlertaConfigUserRepository(Client supabase) : base(supabase)
        {
        }
    }
    public class AlertaHeaderRepository : Repository<AlertaHeader>
    {
        public AlertaHeaderRepository(Client supabase) : base(supabase)
        {
        }
    }
}
