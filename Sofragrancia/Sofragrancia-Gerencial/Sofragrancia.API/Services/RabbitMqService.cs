using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Sofragrancia.API.Services
{
    public class RabbitMqService
    {
        private readonly string _host;
        public RabbitMqService(IConfiguration configuration)
        {
            _host = configuration["MQ:Url"]!;
        }

        public async Task PublicarEmailTrocaSenha(string email, string novaSenha)
        {
            var factory = new ConnectionFactory() { HostName = _host };
            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue: "fila_recuperacao_senha", durable: true, exclusive: false, autoDelete: false, arguments: null);

            var payload = new { Email = email, NovaSenha = novaSenha };
            string mensagemJson = JsonSerializer.Serialize(payload);
            var body = Encoding.UTF8.GetBytes(mensagemJson);
            var properties = new BasicProperties
            {
                Persistent = true
            };

            await channel.BasicPublishAsync(
                exchange: string.Empty,
                routingKey: "fila_recuperacao_senha",
                mandatory: false,
                basicProperties: properties,
                body: body);
        }

    }
}
