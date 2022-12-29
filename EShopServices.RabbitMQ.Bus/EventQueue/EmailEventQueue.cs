using EShopServices.RabbitMQ.Bus.Events;

namespace EShopServices.RabbitMQ.Bus.EventQueue;

public class EmailEventQueue : Event
{
    public string Destination { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }

    public EmailEventQueue(string destination, string title, string content)
    {
        Destination = destination;
        Title = title;
        Content = content;
    }

}
