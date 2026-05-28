using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazored.LocalStorage;
using Sofragrancia.UI.Services;

namespace Sofragrancia.UI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7152/") });
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddScoped<TokenService>();
            builder.Services.AddScoped<HttpService>();
            builder.Services.AddScoped<NavigationService>();

            await builder.Build().RunAsync();
        }
    }
}
