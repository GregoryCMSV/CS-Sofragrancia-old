using Sofragrancia.Banco;
using Sofragrancia_EmailSender.Process;
using Sofragrancia_EmailSender.Services;

namespace Sofragrancia_EmailSender
{
    public class Program
    {

        //Usado só quando roda o worker sozinho
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);
            builder.Services.AddSingleton<EmailService>();
            builder.Services.AddHostedService<AlertSenderWorker>();
            builder.Services.AddHostedService<RecuperacaoSenhaWorker>();
            var host = builder.Build();
            host.Run();
        }
    }
}
