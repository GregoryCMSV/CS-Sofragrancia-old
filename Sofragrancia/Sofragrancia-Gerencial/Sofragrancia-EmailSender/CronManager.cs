using Cronos;
using Microsoft.Extensions.Configuration;

namespace B1Worker.Core.Helpers
{
    public class CronManager
    {
        private readonly ILogger _logger;
        private readonly CronExpression _cronExpression;

        public CronManager(IConfiguration configuration, ILogger<CronManager> logger, string configKey = "Cron")
        {

            var cronString = configuration[configKey];
            if (string.IsNullOrWhiteSpace(cronString))
            {
                throw new ArgumentNullException(nameof(configuration), "A propriedade 'Cron' não foi encontrada no appsettings.json.");
            }

            _logger = logger;
            _cronExpression = CronExpression.Parse(cronString, CronFormat.Standard);
        }

        /// <summary>
        /// Calcula o tempo até a próxima execução, gera o log e aguarda (await) o tempo necessário.
        /// </summary>
        public async Task WaitForNextScheduleAsync(CancellationToken stoppingToken)
        {
            var utcNow = DateTime.UtcNow;

            var nextUtc = _cronExpression.GetNextOccurrence(utcNow);

            if (!nextUtc.HasValue)
            {
                _logger.LogWarning("Não foi possível calcular a próxima execução do Cron. O Worker ficará inativo.");
                await Task.Delay(Timeout.Infinite, stoppingToken);
                return;
            }

            var delay = nextUtc.Value - utcNow;
            var nextLocalTime = nextUtc.Value.ToLocalTime();

            _logger.LogInformation($"Próxima execução agendada para: {nextLocalTime:dd/MM/yyyy HH:mm:ss}.");
            if (delay > TimeSpan.Zero)
            {
                await Task.Delay(delay, stoppingToken);
            }
        }
    }
}
