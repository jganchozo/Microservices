using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ServiceShop.RabbitMQ.Bus.Bus;
using ServiceShop.RabbitMQ.Bus.Commands;
using ServiceShop.RabbitMQ.Bus.Events;
using System.Text;

namespace ServiceShop.RabbitMQ.Bus.Implement
{
    public class RabbitEventBus : IRabbitEventBus
    {
        private readonly IMediator _mediator;
        private readonly Dictionary<string, List<Type>> _handlers;
        private readonly List<Type> _typeEvent;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IConfiguration _configuration;

        public RabbitEventBus(IMediator mediator, IServiceScopeFactory serviceScopeFactory, IConfiguration configuration)
        {
            _mediator = mediator;
            _handlers = new Dictionary<string, List<Type>>();
            _typeEvent = new List<Type>();
            _serviceScopeFactory = serviceScopeFactory;
            _configuration = configuration;
        }

        public void Publish<T>(T @event) where T : Event
        {
            var factory = new ConnectionFactory()
            {
                HostName = "rabbitmq",
                UserName = _configuration.GetSection("RabbitMQ")["username"]!,
                Password = _configuration.GetSection("RabbitMQ")["password"]!
            };

            using (var connection = factory.CreateConnection())
            {
                using var channel = connection.CreateModel();
                var eventName = @event.GetType().Name;

                channel.QueueDeclare(eventName, false, false, false, null);

                var message = JsonConvert.SerializeObject(@event);
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(string.Empty, eventName, null, body);
            }
        }

        public Task SendCommand<T>(T command) where T : Command
        {
            return _mediator.Send(command);
        }

        public void Subscribe<T, TH>()
            where T : Event
            where TH : IEventHandler<T>
        {
            var eventName = typeof(T).Name;
            var typeEventHandler = typeof(TH);

            if (!_typeEvent.Contains(typeof(T)))
            {
                _typeEvent.Add(typeof(T));
            }

            if (!_handlers.ContainsKey(eventName))
            {
                _handlers.Add(eventName, new List<Type>());
            }

            if (_handlers[eventName].Any(x => x.GetType() == typeEventHandler))
            {
                throw new ArgumentException($"Handler {typeEventHandler.Name} was registered before by {eventName}");
            }

            _handlers[eventName].Add(typeEventHandler);

            var factory = new ConnectionFactory()
            {
                HostName = "rabbitmq",
                UserName = _configuration.GetSection("RabbitMQ")["username"]!,
                Password = _configuration.GetSection("RabbitMQ")["password"]!,
                DispatchConsumersAsync = true
            };

            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.QueueDeclare(eventName, false, false, false, null);

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.Received += Consumer_Delegate;

            channel.BasicConsume(eventName, true, consumer);
        }

        private async Task Consumer_Delegate(object sender, BasicDeliverEventArgs e)
        {
            var eventName = e.RoutingKey;
            var message = Encoding.UTF8.GetString(e.Body.ToArray());

            try
            {
                if (_handlers.ContainsKey(eventName))
                {
                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var subscriptions = _handlers[eventName];

                        foreach (var item in subscriptions)
                        {
                            var handler = scope.ServiceProvider.GetService(item); //Activator.CreateInstance(item);

                            if (handler is null)
                            {
                                continue;
                            }

                            var eventType = _typeEvent.SingleOrDefault(x => x.Name == eventName);
                            var dsEvent = JsonConvert.DeserializeObject(message, eventType);
                            var concretType = typeof(IEventHandler<>).MakeGenericType(eventType);

                            await (Task)concretType.GetMethod("Handle").Invoke(handler, new object[] { dsEvent });
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
