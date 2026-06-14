using Sofragrancia.Banco.Models.Alertas;
using Supabase;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sofragrancia_EmailSender.Interfaces
{
    public interface IAlertStrategy
    {
        Task<string> GenerateHtmlAlertAsync(Client client, AlertaConfigUser config);
    }
}
