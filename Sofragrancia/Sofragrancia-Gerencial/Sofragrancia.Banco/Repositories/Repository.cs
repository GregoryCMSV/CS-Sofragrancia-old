using Sofragrancia.Banco.Interfaces;
using Sofragrancia.Banco.Models;
using Sofragrancia.Banco.Models.Alertas;
using Supabase;
using Supabase.Postgrest.Models;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;
using System.Text.Json;
using static Supabase.Postgrest.Constants;

namespace Sofragrancia.Banco.Repositories
{
    public abstract class Repository<T> where T :  BaseModel, IEntidadeBase, new()
    {
        protected readonly Client _supabase;

        private static readonly PropertyInfo[] _propriedadesLimpas;

        public static PropertyInfo[] PropriedadesLimpas { get => _propriedadesLimpas.ToArray(); private set;  }

        static Repository()
        {
            _propriedadesLimpas = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.DeclaringType == typeof(T))
                .ToArray();
        }

        public Repository(Client supabase)
        {
            _supabase = supabase;
        }

        #region Old
        [Obsolete("Use GetAllModelsAsync")]
        public virtual async Task<List<T>> GetAllAsync()
        {
            var response = await _supabase.From<T>().Get();
            return response.Models;
        }
        [Obsolete("Use InsertModelsAsync")]
        public virtual async Task<T> InsertAsync(T produto)
        {
            var response = await _supabase.From<T>().Insert(produto);
            return response.Models.FirstOrDefault();
        }
        [Obsolete("Use GetModelByIDAsync")]
        public virtual async Task<T> GetByIDAsync(int id)
        {
            var response = await _supabase.From<T>()
                                          .Where(x => x.Id == id)
                                          .Get();
            return response.Models.FirstOrDefault();
        }
        #endregion

        protected dynamic GenerateCleanObject(object item, Type tipo = null)
        {
            if (item == null) return null;

            tipo ??= item.GetType();
            var expando = new ExpandoObject() as IDictionary<string, object>;

            var propriedades = tipo.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                   .Where(p => p.DeclaringType == tipo);

            foreach (var prop in propriedades)
            {
                var valor = prop.GetValue(item);

                if (valor != null && typeof(IEntidadeBase).IsAssignableFrom(prop.PropertyType))
                {
                    expando[prop.Name] = GenerateCleanObject(valor, prop.PropertyType);
                    continue;
                }

                if (valor is IEnumerable lista  && prop.PropertyType != typeof(string))
                {
                    var tipoDaLista = prop.PropertyType.IsGenericType
                                      ? prop.PropertyType.GetGenericArguments()[0]
                                      : null;

                    if (tipoDaLista != null && typeof(IEntidadeBase).IsAssignableFrom(tipoDaLista))
                    {
                        var listaLimpa = new List<dynamic>();
                        foreach (var subItem in lista)
                        {
                            listaLimpa.Add(GenerateCleanObject(subItem, tipoDaLista));
                        }
                        expando[prop.Name] = listaLimpa;
                        continue; 
                    }
                }

                expando[prop.Name] = valor;
            }

            return expando;
        }

        public virtual async Task<dynamic> InsertModelAsync(T produto)
        {
            var response = await _supabase.From<T>().Insert(produto);
            var item = response.Models.FirstOrDefault();
            if (item == null)
            {
                return null;
            }

            return GenerateCleanObject(item,typeof(T));
        }

        public virtual async Task<dynamic> UpsertModelAsync(T produto)
        {
            var response = await _supabase.From<T>().Upsert(produto);
            var item = response.Models.FirstOrDefault();
            if (item == null)
            {
                return null;
            }

            return GenerateCleanObject(item,typeof(T));
        }

        public virtual async Task<List<dynamic>> GetAllModelAsync()
        {
            var response = await _supabase.From<T>().Get();
            var listaLimpa = new List<dynamic>();

            foreach (var item in response.Models)
            {
                listaLimpa.Add(GenerateCleanObject(item, typeof(T)));
            }

            return listaLimpa;
        }
        public virtual async Task<dynamic> GetModelByIDAsync(int id)
        {
            var response = await _supabase.From<T>()
                                  .Filter("id", Operator.Equals, id)
                                  .Get();

            var item = response.Models.FirstOrDefault();
            if (item == null)
            {
                return null;
            }

            return GenerateCleanObject(item,typeof(T));
        }

        public virtual async Task<dynamic> UpdateByID(int id, Dictionary<string, object> atualizacoes)
        {
            atualizacoes.Remove("id");
            atualizacoes.Remove("Id");
            var response = await _supabase.From<T>()
                                          .Filter("id", Operator.Equals, id)
                                          .Get();

            var itemAtual = response.Models.FirstOrDefault();
            if (itemAtual == null) return null;

            var tipo = typeof(T);
            foreach (var atualizacao in atualizacoes)
            {
                var prop = tipo.GetProperty(atualizacao.Key, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (prop != null && prop.CanWrite && prop.DeclaringType == typeof(T))
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

            var updateResponse = await _supabase.From<T>().Update(itemAtual);
            var itemAtualizado = updateResponse.Models.FirstOrDefault();

            return GenerateCleanObject(itemAtualizado,typeof(T));
        }

        public async Task<dynamic> GetByEmailAsync(string email)
        {
            var id = _supabase.Auth.CurrentUser.Id;
            var response = await _supabase.From<AlertaHeader>()
                                          .Where(x => x.IdUsuario == id)
                                          .Get();

            var item = response.Models.FirstOrDefault();

            if (item == null)
            {
                return null;
            }

            return GenerateCleanObject(item, typeof(AlertaHeader));
        }

        public virtual async Task<dynamic> PatchByID(int id, Dictionary<string, object> atualizacoes)
        {

            
            return await UpdateByID(id, atualizacoes);
        }
    }
}
