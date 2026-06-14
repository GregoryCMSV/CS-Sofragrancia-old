using Sofragrancia.Banco;
using Sofragrancia_EmailSender.Services;

namespace Sofragrancia_EmailSender
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);
            builder.Services.AddSingleton<EmailService>();
            builder.Services.AddHostedService<Worker>();
            var host = builder.Build();
            host.Run();
        }
    }
}
