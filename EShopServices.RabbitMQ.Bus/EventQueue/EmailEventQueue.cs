using EShopServices.RabbitMQ.Bus.Events;

namespace EShopServices.RabbitMQ.Bus.EventQueue;

public class EmailEventQueue : Event
{
    public string Destination { get; set; }
    public string Subject { get; set; }
    public string Content { get; set; }

    public EmailEventQueue(string destination, string subject, string content)
    {
        Destination = destination;
        Subject = subject;
        Content = content;
    }

}
