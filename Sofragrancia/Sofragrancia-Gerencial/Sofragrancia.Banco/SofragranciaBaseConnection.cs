using Supabase;
using System.Runtime.CompilerServices;

namespace Sofragrancia.Banco
{
    public class SofragranciaBaseConnection
    {
        private string url = "";
        private string publicKey = "";
        private static SofragranciaBaseConnection _instance;
        public Client SupabaseClient { get; private set; }

        private SofragranciaBaseConnection()
        {
            SupabaseOptions options = new SupabaseOptions
            {
                AutoConnectRealtime = true
            };

            SupabaseClient = new Client(url, publicKey, options);            
        }

        private SofragranciaBaseConnection(string url, string publicKey)
        {
            SupabaseOptions options = new SupabaseOptions
            {
                AutoConnectRealtime = true
            };

            SupabaseClient = new Client(url, publicKey, options);            
        }

        public static async Task<SofragranciaBaseConnection> GetInstanceAsync()
        {
            if (_instance == null)
            {
                _instance = new SofragranciaBaseConnection();
                await _instance.SupabaseClient.InitializeAsync();
            }
            return _instance;
        }

        public static async Task<SofragranciaBaseConnection> GetInstanceAsync(string url, string publicKey)
        {
            if (_instance == null)
            {
                _instance = new SofragranciaBaseConnection(url,publicKey);
                await _instance.SupabaseClient.InitializeAsync();
            }
            return _instance;
        }
    }
}
