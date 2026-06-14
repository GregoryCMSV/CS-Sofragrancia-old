using Sofragrancia.Banco.Models;
using Sofragrancia.Banco.Models.Alertas;
using Supabase;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;
using System.Text;
using System.Text.Json;
using static Supabase.Postgrest.Constants;

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
                try
                {
                    var insertHeaderResponse = await _supabase.From<AlertaHeader>().Insert(novoHeader);
                    header = insertHeaderResponse.Models.FirstOrDefault();
                }
                catch (Exception e)
                {

                }
            }

            return header;
        }

        public async Task<(List<AlertaBase> AlertasBase, List<AlertaConfigUser> LinhasAtuais)> FindCurrentAlerts(int idHeader)
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
                        UnidadeMedida = alertaBase.UnidadeMedida,
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
        public async Task<bool> HeaderExists(int id)
        {
            var headerResponse = await _supabase.From<AlertaHeader>()
                .Where(h => h.Id == id)
                .Get();

            var header = headerResponse.Models.FirstOrDefault();

            return header != null;
        }

        public async Task<bool> LineExists(int idlinha)
        {
            var lineResponse = await _supabase.From<AlertaConfigUser>()
            .Where(h => h.Id == idlinha)
            .Get();

            var line = lineResponse.Models.FirstOrDefault();

            return line != null;
        }

        public virtual async Task<dynamic> UpdateLineByID(int id, Dictionary<string, object> atualizacoes)
        {
            atualizacoes.Remove("id");
            atualizacoes.Remove("Id");
            var response = await _supabase.From<AlertaConfigUser>()
                                          .Filter("id", Operator.Equals, id)
                                          .Get();

            var itemAtual = response.Models.FirstOrDefault();
            if (itemAtual == null) return null;

            var tipo = typeof(AlertaConfigUser);
            foreach (var atualizacao in atualizacoes)
            {
                var prop = tipo.GetProperty(atualizacao.Key, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (prop != null && prop.CanWrite && prop.DeclaringType == typeof(AlertaConfigUser))
                {
                    object valorFinal = atualizacao.Value;

                    if (valorFinal is JsonElement jsonElement)
                    {
                        valorFinal = jsonElement.Deserialize(prop.PropertyType);
                    }
                    else if (valorFinal != null)
                    {
                        var tipoDestino = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                        valorFinal = Convert.ChangeType(valorFinal, tipoDestino);
                    }

                    prop.SetValue(itemAtual, valorFinal);
                }
            }

            var updateResponse = await _supabase.From<AlertaConfigUser>().Update(itemAtual);
            var itemAtualizado = updateResponse.Models.FirstOrDefault();

            return GenerateCleanObject(itemAtualizado,typeof(AlertaConfigUser));
        }

    }
}

