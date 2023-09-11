using ServiceShop.RabbitMQ.Bus.Events;

namespace ServiceShop.RabbitMQ.Bus.Bus
{
    public interface IEventHandler<in TEvent> : IEventHandler where TEvent : Event
    {
        Task Handle(TEvent @event);
    }

    public interface IEventHandler { }

}
