using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Sofragrancia_EmailSender.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Sofragrancia_EmailSender.Process
{
    public class RecuperacaoSenhaWorker : BackgroundService
    {
        private readonly ILogger<RecuperacaoSenhaWorker> _logger;
        private IConnection? _connection;
        private IChannel? _channel;
        private EmailService _emailService;
        private IConfiguration _configuration;

        public RecuperacaoSenhaWorker(ILogger<RecuperacaoSenhaWorker> logger, EmailService emailService, IConfiguration configuration)
        {
            _logger = logger;
            _emailService = emailService;
            _configuration = configuration;
        }


        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            var factory = new ConnectionFactory { Uri = new Uri(_configuration["MQ:Url"]!) };
            _connection = await factory.CreateConnectionAsync();
            _channel = await _connection.CreateChannelAsync();

            await _channel.QueueDeclareAsync(queue: "fila_recuperacao_senha", durable: true, exclusive: false, autoDelete: false, arguments: null);

            await base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (_channel == null) return;
            var consumer = new AsyncEventingBasicConsumer(_channel);

            _logger.LogWarning("Enviando email para trocar a senha");

            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var mensagemJson = Encoding.UTF8.GetString(body);

                try
                {
                    using var jsonDoc = JsonDocument.Parse(mensagemJson);
                    var email = jsonDoc.RootElement.GetProperty("Email").GetString();
                    var novaSenha = jsonDoc.RootElement.GetProperty("NovaSenha").GetString();

                    string html = $"<h1>Recuperação de Senha</h1><p>A sua nova senha é: <b>{novaSenha}</b></p>";
                    await _emailService.SendEmailAsync(email, "A sua Nova Senha - Sofragrância", html);

                    await _channel.BasicAckAsync(deliveryTag: ea.DeliveryTag, multiple: false);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Erro ao enviar e-mail. A mensagem volta para a fila. Erro: {ex.Message}");
                    await _channel.BasicNackAsync(deliveryTag: ea.DeliveryTag, multiple: false, requeue: true);
                }
            };

            await _channel.BasicConsumeAsync(queue: "fila_recuperacao_senha", autoAck: false, consumer: consumer);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            if (_channel is not null) await _channel.CloseAsync();
            if (_connection is not null) await _connection.CloseAsync();
            await base.StopAsync(cancellationToken);
        }
    }
}
