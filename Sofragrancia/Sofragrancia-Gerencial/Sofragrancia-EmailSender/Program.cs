using Sofragrancia.Banco;
using Sofragrancia_EmailSender.Process;
using Sofragrancia_EmailSender.Services;
using Microsoft.Extensions.Hosting;
using B1Worker.Core.Helpers;
using B1Worker.Core.Helpers.Loggers;

namespace Sofragrancia_EmailSender
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);
            builder.Services.AddWindowsService(options =>
            {
                options.ServiceName = "Sofragrancia Email Worker";
            });
            var supabaseUrl = builder.Configuration["Supabase:Url"];
            var supabaseKey = builder.Configuration["Supabase:Key"];
            var baseInstance = SofragranciaBaseConnection.GetInstanceAsync(supabaseUrl, supabaseKey).Result;
            builder.Services.AddScoped(_ => baseInstance.SupabaseClient);
            builder.Services.AddSingleton<EmailService>();
            builder.Services.AddSingleton<Logger>();
            builder.Services.AddSingleton<CronManager>();
            builder.Services.AddHostedService<AlertSenderWorker>();
            builder.Services.AddHostedService<RecuperacaoSenhaWorker>();
            var host = builder.Build();
            host.Run();
        }
    }
}
