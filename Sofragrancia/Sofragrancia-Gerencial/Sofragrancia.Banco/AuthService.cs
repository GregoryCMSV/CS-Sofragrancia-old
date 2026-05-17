using Supabase;
using Supabase.Gotrue;
using Supabase.Interfaces;
using System.Data;

namespace Sofragrancia.Banco
{
    public class AuthService
    {
        private readonly Supabase.Client _supabase;

        public AuthService(Supabase.Client supabase)
        {
            _supabase = supabase;
        }

        public Dictionary<string, object> GetDefaultUserOptions => new Dictionary<string, object>()
        {
            {"role",""},
            {"nomeCompleto",""}
        };

        public async Task<Session> CadastrarUsuarioAsync(string email, string senha, Dictionary<string,object> metadados = null)
        {
            var opcoes = new SignUpOptions
            {
                Data = metadados ?? GetDefaultUserOptions
            };
            var session = await _supabase.Auth.SignUp(email, senha, opcoes);
            return session;
        }

        public async Task<Session> LoginAsync(string email, string senha)
        {
            var session = await _supabase.Auth.SignIn(email, senha);
            return session;
        }

        public async Task<User> AtualizarUsuarioAsync(string novoEmail = null, string novaSenha = null, Dictionary<string, object> dadosExtras = null)
        {
            var atributos = new UserAttributes();

            if (!string.IsNullOrEmpty(novoEmail))
                atributos.Email = novoEmail;

            if (!string.IsNullOrEmpty(novaSenha))
                atributos.Password = novaSenha;

            if (dadosExtras != null)
                atributos.Data = dadosExtras;

            var userResponse = await _supabase.Auth.Update(atributos);

            return userResponse;
        }

        public async Task DeslogarAsync()
        {
            await _supabase.Auth.SignOut();
        }
    }
}
