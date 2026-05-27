using Sofragrancia.Banco.Interfaces;
using Sofragrancia.Banco.Models;
using Supabase;
using Supabase.Postgrest.Models;
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
        
        public virtual async Task<dynamic> InsertModelAsync(T produto)
        {
            var response = await _supabase.From<T>().Insert(produto);
            var item = response.Models.FirstOrDefault();
            if (item == null)
            {
                return null;
            }

            var expando = new ExpandoObject() as IDictionary<string, object>;

            foreach (var prop in _propriedadesLimpas)
            {
                expando[prop.Name] = prop.GetValue(item);
            }

            return expando;
        }

        public virtual async Task<List<dynamic>> GetAllModelAsync()
        {
            var response = await _supabase.From<T>().Get();
            var listaLimpa = new List<dynamic>();

            foreach (var item in response.Models)
            {
                var expando = new ExpandoObject() as IDictionary<string, object>;

                foreach (var prop in _propriedadesLimpas)
                {
                    expando[prop.Name] = prop.GetValue(item);
                }

                listaLimpa.Add(expando);
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

            var expando = new ExpandoObject() as IDictionary<string, object>;

            foreach (var prop in _propriedadesLimpas)
            {
                expando[prop.Name] = prop.GetValue(item);
            }

            return expando;
        }

        public virtual async Task<dynamic> UpdateByID(int id, Dictionary<string, object> atualizacoes)
        {
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

            var expando = new ExpandoObject() as IDictionary<string, object>;
            foreach (var prop in _propriedadesLimpas) 
            {
                expando[prop.Name] = prop.GetValue(itemAtualizado);
            }

            return expando;
        }
    }
}
