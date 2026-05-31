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

        public async Task SincronizarAlertasDoUsuarioAsync(string idUsuarioAuth, string emailUsuario)
        {
            var header = await CheckHeader(idUsuarioAuth, emailUsuario);
            if (header == null) return;

            var (alertasBase, linhasAtuais) = await FindCurrentAlerts(header.Id);
            if (alertasBase == null || !alertasBase.Any()) return;

            await InsertAlertLines(header.Id, alertasBase, linhasAtuais);
        }

        private async Task<AlertaHeader> CheckHeader(string idUsuarioAuth, string emailUsuario)
        {
            var headerResponse = await _supabase.From<AlertaHeader>()
                                                .Where(h => h.IdUsuario == idUsuarioAuth)
                                                .Get();

            var header = headerResponse.Models.FirstOrDefault();

            if (header == null)
            {
                var novoHeader = new AlertaHeader
                {
                    IdUsuario = idUsuarioAuth,
                    Email = emailUsuario,
                    Horario = new TimeOnly(8, 0),
                    Dias = new int[] { 1, 2, 3, 4, 5 },
                    CriadoEm = DateTime.UtcNow,
                    AtualizadoEm = DateTime.UtcNow,
                    IsEnable = true
                };

                var insertHeaderResponse = await _supabase.From<AlertaHeader>().Insert(novoHeader);
                header = insertHeaderResponse.Models.FirstOrDefault();
            }

            return header;
        }

        private async Task<(List<AlertaBase> AlertasBase, List<AlertaConfigUser> LinhasAtuais)> FindCurrentAlerts(int idHeader)
        {
            var baseResponse = await _supabase.From<AlertaBase>().Get();
            var alertasBase = baseResponse.Models ?? new List<AlertaBase>();

            var atuaisResponse = await _supabase.From<AlertaConfigUser>()
                                                .Where(a => a.IdHeader == idHeader)
                                                .Get();
            var linhasAtuais = atuaisResponse.Models ?? new List<AlertaConfigUser>();

            return (alertasBase, linhasAtuais);
        }

        private async Task InsertAlertLines(int idHeader, List<AlertaBase> alertasBase, List<AlertaConfigUser> linhasAtuais)
        {
            var linhasParaInserir = new List<AlertaConfigUser>();

            foreach (var alertaBase in alertasBase)
            {
                if (!linhasAtuais.Any(linha => linha.IdAlertaBase == alertaBase.Id))
                {
                    linhasParaInserir.Add(new AlertaConfigUser
                    {
                        IdHeader = idHeader,
                        IdAlertaBase = alertaBase.Id,
                        Trigger = alertaBase.Trigger,
                        Value = alertaBase.Value,
                        CriadoEm = DateTime.UtcNow,
                        AtualizadoEm = DateTime.UtcNow,
                        IsEnable = true
                    });
                }
            }

            if (linhasParaInserir.Any())
            {
                await _supabase.From<AlertaConfigUser>().Insert(linhasParaInserir);
            }
        }
    }
}

