using Sofragrancia.Banco.Models;
using Supabase;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace Sofragrancia.Banco.Repositories
{
    public class PedidoRepository : Repository<Pedido>
    {
        public PedidoRepository(Client supabase) : base(supabase)
        {
        }

        public override async Task<List<dynamic>> GetAllModelAsync()
        {
            var response = await _supabase.From<Pedido>().Select("*, item_pedido(*)")
                .Get();
            var listaLimpa = new List<dynamic>();

            foreach (var item in response.Models)
            {
                var expando = new ExpandoObject() as IDictionary<string, object>;

                foreach (var prop in PropriedadesLimpas)
                {
                    expando[prop.Name] = prop.GetValue(item);
                }

                listaLimpa.Add(expando);
            }

            return listaLimpa;
        }

        public override async Task<dynamic> GetModelByIDAsync(int id)
        {
            var response = await _supabase.From<Pedido>()
                .Select("*, item_pedido(*)")
                .Where(p => p.Id == id)
                .Single();
            return response;
        }
    }
}
