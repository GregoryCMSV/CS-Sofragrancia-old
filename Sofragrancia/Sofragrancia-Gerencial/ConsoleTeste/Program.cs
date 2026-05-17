using System;
using System.Threading.Tasks;
using Sofragrancia.Banco;
using Sofragrancia.Banco.Models;
using Sofragrancia.Banco.Repositories;

namespace Sofragrancia.ConsoleTest
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("--- Iniciando testes com Autenticação no Supabase ---\n");

            string supabaseUrl = "https://uhgzwpwfojcgrlezjpta.supabase.co/";
            string supabaseKey = "sb_publishable_2wjE_l0l4KNwt7VlTHtgHw_nIyefIBB";

            try
            {
                Console.WriteLine("Conectando ao banco");
                var conexao = await SofragranciaBaseConnection.GetInstanceAsync(supabaseUrl, supabaseKey);
                var supabaseClient = conexao.SupabaseClient;

                //dados do usuário
                var authService = new AuthService(supabaseClient);
                string emailTeste = "gregorycmsv@gmail.com";
                string senhaTeste = "*****5";

                Console.WriteLine("\nTentando cadastrar o usuário");
                try
                {
                    await authService.CadastrarUsuarioAsync(emailTeste, senhaTeste);
                    Console.WriteLine("Usuário cadastrado com sucesso!");
                }
                catch (Exception ex)
                {
                    // Se o e-mail já existir no Supabase, ele lança uma exceção. 
                    // Vamos capturar e seguir para o login normalmente.
                    Console.WriteLine($"Aviso no cadastro: {ex.Message} (Prosseguindo para login...)");
                }

                Console.WriteLine("\nFazendo Login");
                var sessao = await authService.LoginAsync(emailTeste, senhaTeste);
                Console.WriteLine($"Login efetuado! Usuário Autenticado: {sessao.User?.Email}");
                Console.WriteLine($"Token JWT: {sessao.AccessToken}\n");


                Console.WriteLine("Inserindo um novo produto");
                var produtoRepo = new ProdutoRepository(supabaseClient);

                var novoProduto = new Produto
                {
                    IdFornecedor = 1,
                    Descricao = "Perfume Onze de setembro",
                    Marca = "Sofragrância",
                    unidade = "UN",
                    PrecoCusto = "11.09",
                    PrecoVenda = "1109.01",
                    EstoqueAtual = "400",
                    EstoqueMinimo = "400",
                    Categoria = "Perfumaria",
                    CriadoEm = DateTime.UtcNow,
                    AtualizadoEm = DateTime.UtcNow,
                    IsEnable = true
                };

                var produtoInserido = await produtoRepo.InserirAsync(novoProduto);
                Console.WriteLine($"Produto inserido com sucesso! ID no banco: {produtoInserido?.Id}\n");

                
                Console.WriteLine("Buscando todos os produtos do banco");
                var listaProdutos = await produtoRepo.ObterTodosAsync();

                if (listaProdutos != null && listaProdutos.Count > 0)
                {
                    foreach (var p in listaProdutos)
                    {
                        Console.WriteLine($" -> [{p.Id}] {p.Descricao} | Categoria: {p.Categoria} | Venda: R$ {p.PrecoVenda}");
                    }
                }
                else
                {
                    Console.WriteLine("Nenhum produto foi encontrado.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nERRO: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Detalhes: {ex.InnerException.Message}");
                }
            }

            Console.WriteLine("\nIsso é tudo pessoal");
            Console.ReadKey();
        }
    }
}