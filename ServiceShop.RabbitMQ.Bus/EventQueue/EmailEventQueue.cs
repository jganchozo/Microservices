using ServiceShop.RabbitMQ.Bus.Events;

namespace ServiceShop.RabbitMQ.Bus.QueueEvent
{
    public class EmailEventQueue : Event
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public EmailEventQueue(string to, string subject, string body)
        {
            To = to;
            Subject = subject;
            Body = body;
        }
    }
}
