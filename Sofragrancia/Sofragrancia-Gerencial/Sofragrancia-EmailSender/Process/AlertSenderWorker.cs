using Sofragrancia.Banco;
using Sofragrancia.Banco.Models.Alertas;
using Sofragrancia.Banco.Repositories;
using Sofragrancia_EmailSender.Factory;
using Sofragrancia_EmailSender.Services;
using Supabase;
using System.Text;
using static Supabase.Postgrest.Constants;

namespace Sofragrancia_EmailSender.Process
{
    public class AlertSenderWorker : BackgroundService
    {
        private readonly ILogger<AlertSenderWorker> _logger;
        private readonly IServiceProvider _serviceProvider;
        private string _supaKey;
        private string _supaUrl;
        private EmailService _emailService;
        private DateTime _current;


        public AlertSenderWorker(ILogger<AlertSenderWorker> logger, IServiceProvider serviceProvider, IConfiguration configuration, EmailService emailService)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _supaUrl = configuration["Supabase:Url"]!;
            _supaKey = configuration["Supabase:Key"]!;
            _emailService = emailService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var timer = new PeriodicTimer(TimeSpan.FromMinutes(1));
            //using var timer = new PeriodicTimer(TimeSpan.FromSeconds(1));

            while (await timer.WaitForNextTickAsync(stoppingToken))
            {
                _logger.LogInformation("Verificando alertas às: {time}", DateTimeOffset.Now.AddHours(-3));
                _current = DateTime.Today;
                try
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var client = (await SofragranciaBaseConnection.GetInstanceAsync(_supaUrl, _supaKey)).SupabaseClient;
                        var alertas = await GetAlertsFromToday(client);
                        _logger.LogInformation($"Hora: {_current.Hour} / {DateTimeOffset.Now.AddHours(-3)}");
                        var alertasAgora = alertas.Where(a => a.Horario.Minute == _current.Minute && a.Horario.Hour == _current.Hour).ToList();

                        foreach (var alert in alertasAgora)
                        {
                            var alertasAtivos = alert.Alertas.Where(a => a.IsEnable).OrderBy(a => a.IdAlertaBase);
                            var htmlPartes = new List<string>();
                            foreach (var config in alertasAtivos)
                            {
                                var strategy = AlertaStrategyFactory.ObterStrategy(config.IdAlertaBase);

                                if (strategy != null)
                                {
                                    string htmlDoAlerta = await strategy.GenerateHtmlAlertAsync(client, config);

                                    if (!string.IsNullOrWhiteSpace(htmlDoAlerta))
                                    {
                                        htmlPartes.Add(htmlDoAlerta);
                                    }
                                }
                            }

                            if (htmlPartes.Any())
                            {
                                string htmlFinal = CreateEmailBody(htmlPartes); 
                                await _emailService.SendEmailAsync(alert.Email, "Seus de Alertas - Sofragrância", htmlFinal);
                                _logger.LogInformation($"Email de alertas enviado para {alert.Email}");
                            }

                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erro ao processar rotina de emails.");
                }
            }
        }

        private string CreateEmailBody(List<string> htmlPartes)
        {
            StringBuilder sb = new();
            sb.AppendLine("<!DOCTYPE html>");
            sb.AppendLine("<html lang='pt-BR'>");
            sb.AppendLine("<head>");
            sb.AppendLine("<meta charset='UTF-8'>");
            sb.AppendLine("<style>");

            sb.AppendLine(@"
                    body { font-family: Arial, sans-serif; background-color: #f4f4f4; padding: 20px; color: #333; }
                    .email-container { max-width: 800px; margin: 0 auto; background-color: #ffffff; padding: 20px; border-radius: 8px; box-shadow: 0 2px 4px rgba(0,0,0,0.1); }
        
                    /* Caixas Principais */
                    .alert-box { margin-bottom: 20px; padding: 15px; border-left: 4px solid #ccc; background-color: #f9f9f9; }
                    .alert-box.basic { border-left: none; background-color: transparent; padding: 0; }
                    .alert-box.danger { border-left-color: #d9534f; background-color: #f2dede; }
                    .alert-box.warning { border-left-color: #f0ad4e; background-color: #fcf8e3; }
                    .alert-box.info { border-left-color: #31708f; background-color: #d9edf7; }
        
                    /* Títulos e Subtítulos */
                    .alert-title { margin-top: 0; margin-bottom: 10px; font-size: 18px; font-weight: bold; }
                    .alert-title.danger { color: #d9534f; }
                    .alert-title.warning { color: #d58512; }
                    .alert-title.info { color: #245269; }
        
                    .alert-subtitle { margin-top: -5px; margin-bottom: 15px; font-size: 14px; font-style: italic; }
                    .alert-subtitle.danger { color: #a94442; }
                    .alert-subtitle.warning { color: #8a6d3b; }
        
                    /* Textos e Listas */
                    .alert-text.info { color: #31708f; }
                    .alert-list { color: #333; margin-top: 0; padding-left: 20px; }
                    .alert-list.unstyled { list-style-type: none; padding-left: 0; }
        
                    /* Destaques dinâmicos */
                    .text-danger { color: #d9534f; font-weight: bold; }
                    .text-success { color: #5cb85c; font-weight: bold; }
                ");

            sb.AppendLine("</style>");
            sb.AppendLine("</head>");
            sb.AppendLine("<body>");
            sb.AppendLine("<div class='email-container'>");

            sb.AppendLine($"<h2 style='text-align: center; color: #333;'>Resumo dos Alertas - {DateTime.Today.ToString("dd/MM/yyyy")}</h2>");
            sb.AppendLine("<hr style='border: 1px solid #eee; margin-bottom: 20px;' />");

            foreach (var bloco in htmlPartes)
            {
                sb.AppendLine(bloco);
            }

            sb.AppendLine("</div>");
            sb.AppendLine("</body>");
            sb.AppendLine("</html>");

            return sb.ToString();
        }

        private async Task<List<AlertaHeader>> GetAlertsFromToday(Client client)
        {
            var hoje = (int)DateTime.Today.DayOfWeek;
            var alertas = await client.From<AlertaHeader>()
                    .Get();

            var models = alertas.Models;
            return models.Where(a => a.Dias.Contains(hoje) && a.IsEnable).ToList();
        }

    }
}
