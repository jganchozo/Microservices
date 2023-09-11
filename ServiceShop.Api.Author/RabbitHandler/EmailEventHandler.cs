using ServiceShop.Messenger.Email.SendGridLibrary.Interface;
using ServiceShop.Messenger.Email.SendGridLibrary.Model;
using ServiceShop.RabbitMQ.Bus.Bus;
using ServiceShop.RabbitMQ.Bus.QueueEvent;

namespace ServiceShop.Api.Author.RabbitHandler
{
    public class EmailEventHandler : IEventHandler<EmailEventQueue>
    {
        private readonly ILogger<EmailEventHandler> _logger;
        private readonly IEmail _email;
        private readonly IConfiguration _configuration;

        public EmailEventHandler()
        {
            
        }

        public EmailEventHandler(ILogger<EmailEventHandler> logger, IEmail email, IConfiguration configuration)
        {
            _logger = logger;
            _email = email;
            _configuration = configuration;
        }

        public async Task Handle(EmailEventQueue @event)
        {
            _logger.LogInformation($"Value from rabbitmq {@event.Subject} To: {@event.To}");
            SendGridData sendGridData = new()
            {
                To = @event.To,
                Subject = @event.Subject,
                Body = @event.Body,
                Name = @event.To,
                SendGridAPIKey = _configuration.GetSection("SendGrid")["ApiKey"]!
            };

            var result = await _email.Send(sendGridData);

            if (result.Result)
            {
                await Task.CompletedTask;

                return;
            }
        }
    }
}
