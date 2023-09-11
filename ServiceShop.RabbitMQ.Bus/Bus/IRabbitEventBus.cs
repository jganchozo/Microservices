using ServiceShop.RabbitMQ.Bus.Commands;
using ServiceShop.RabbitMQ.Bus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceShop.RabbitMQ.Bus.Bus
{
    public interface IRabbitEventBus
    {
        Task SendCommand<T>(T command) where T : Command;
        void Publish<T>(T @event) where T : Event;
        void Subscribe<T, TH>() where T : Event
                                where TH : IEventHandler<T>;
    }
}
