using Supabase;
using Supabase.Gotrue;
using Supabase.Interfaces;

namespace Sofragrancia.Banco
{
    public class AuthService
    {
        private readonly Supabase.Client _supabase;

        public AuthService(Supabase.Client supabase)
        {
            _supabase = supabase;
        }

        public async Task<Session> CadastrarUsuarioAsync(string email, string senha)
        {
            var session = await _supabase.Auth.SignUp(email, senha);
            return session;
        }

        public async Task<Session> LoginAsync(string email, string senha)
        {
            var session = await _supabase.Auth.SignIn(email, senha);
            return session;
        }

        public async Task DeslogarAsync()
        {
            await _supabase.Auth.SignOut();
        }
    }
}
