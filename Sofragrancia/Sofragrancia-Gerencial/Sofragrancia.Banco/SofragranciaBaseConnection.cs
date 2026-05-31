using Supabase;
using System.Runtime.CompilerServices;

namespace Sofragrancia.Banco
{
    public class SofragranciaBaseConnection
    {
        private string url = "";
        private string publicKey = "";
        private static SofragranciaBaseConnection _instance;
        private string privateKey = "";

        public Client SupabaseClient { get; private set; }

        private SofragranciaBaseConnection()
        {
            SupabaseOptions options = new SupabaseOptions
            {
                AutoConnectRealtime = true
            };

            SupabaseClient = new Client(url, publicKey, options);            
        }

        private SofragranciaBaseConnection(string url, string publicKey, string privateKey)
        {
            SupabaseOptions options = new SupabaseOptions
            {
                AutoConnectRealtime = true
            };

            SupabaseClient = new Client(url, publicKey, options);
            this.privateKey = privateKey;
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

        public static async Task<SofragranciaBaseConnection> GetInstanceAsync(string url, string publicKey, string privateKey = "")
        {
            if (_instance == null)
            {
                _instance = new SofragranciaBaseConnection(url,publicKey, privateKey);
                await _instance.SupabaseClient.InitializeAsync();
            }
            return _instance;
        }
    }
}
