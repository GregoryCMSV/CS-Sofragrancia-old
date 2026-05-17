using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sofragrancia.Banco.Repositories;
using Sofragrancia.Banco.Models;
using Supabase;
using System.Text;

namespace Sofragrancia.API.Controllers
{

    public class ProdutosController : BaseController<Produto, ProdutoRepository>
    {
        public ProdutosController(Client client) : base(client)
        {
            _repository = new ProdutoRepository(client);
        }
    }


}

