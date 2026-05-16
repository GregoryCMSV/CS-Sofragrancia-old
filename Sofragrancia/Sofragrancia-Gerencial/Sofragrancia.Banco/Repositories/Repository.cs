using Sofragrancia.Banco.Interfaces;
using Sofragrancia.Banco.Models;
using Supabase;
using Supabase.Postgrest.Models;
using System.Collections.Generic;

namespace Sofragrancia.Banco.Repositories
{
    public abstract class Repository<T> where T :  BaseModel, IEntidadeBase, new()
    {
        private readonly Client _supabase;

        public Repository(Client supabase)
        {
            _supabase = supabase;
        }

        public virtual async Task<List<T>> ObterTodosAsync()
        {
            var response = await _supabase.From<T>().Get();
            return response.Models;
        }

        public virtual async Task<T> InserirAsync(T produto)
        {
            var response = await _supabase.From<T>().Insert(produto);
            return response.Models.FirstOrDefault();
        }

        public virtual async Task<T> ObterPorIdAsync(int id)
        {
            var response = await _supabase.From<T>()
                                          .Where(x => x.Id == id)
                                          .Get();
            return response.Models.FirstOrDefault();
        }
    }
}
