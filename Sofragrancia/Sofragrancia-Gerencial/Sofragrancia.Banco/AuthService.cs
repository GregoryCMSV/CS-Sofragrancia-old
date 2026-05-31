using Sofragrancia.Shared.DTOs;
using Supabase;
using Supabase.Gotrue;
using Supabase.Interfaces;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;

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

        public bool ValidarMetadadosDoToken(string token, string roleDesejada = null)
        {
            if (string.IsNullOrWhiteSpace(token)) return false;

            token = token.Replace("Bearer ", "");
            var handler = new JwtSecurityTokenHandler();

            if (!handler.CanReadToken(token)) return false;
            var jwtToken = handler.ReadJwtToken(token);

            if (string.IsNullOrEmpty(roleDesejada)) return true;
            var metadata = jwtToken.Claims.FirstOrDefault(c => c.Type == "user_metadata")?.Value;

            return metadata != null && metadata.Contains($"\"Role\":\"{roleDesejada}\"");
        }

        public async Task<string> CadastrarUsuarioInternoAsync(NewUserRequestDto newUser, string serviceRoleKey)
        {
            var metadados = newUser.MetaDados.GetType()
            .GetProperties()
            .ToDictionary(
                p => p.Name,
                p => p.GetValue(newUser.MetaDados) ?? string.Empty
            );

            var atributos = new AdminUserAttributes
            {
                Email = newUser.Email,
                Password = newUser.Senha,
                EmailConfirm = true,
                UserMetadata = metadados
            };
            try
            {
                var admeTop = _supabase.AdminAuth(serviceRoleKey);
                var user = await admeTop.CreateUser(atributos);

                return user?.Id;
            }
            catch (Exception ex)
            {
                return "";
            }
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
