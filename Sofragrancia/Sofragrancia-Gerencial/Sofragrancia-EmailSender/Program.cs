using Sofragrancia.Banco;
using Sofragrancia_EmailSender.Process;
using Sofragrancia_EmailSender.Services;
using Microsoft.Extensions.Hosting;

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
            builder.Services.AddSingleton<EmailService>();
            builder.Services.AddHostedService<AlertSenderWorker>();
            builder.Services.AddHostedService<RecuperacaoSenhaWorker>();
            var host = builder.Build();
            host.Run();
        }
    }
}
