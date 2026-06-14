using Sofragrancia_EmailSender.Process;
using Sofragrancia_EmailSender.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sofragrancia_EmailSender.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddEmailWorker(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<EmailService>();
            services.AddHostedService<AlertSenderWorker>();
            services.AddHostedService<RecuperacaoSenhaWorker>();
            return services;
        }

    }
}
