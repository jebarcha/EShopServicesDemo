using EShopServices.RabbitMQ.Bus.BusRabbit;
using EShopServices.RabbitMQ.Bus.EventQueue;
using System.Runtime.CompilerServices;

namespace EShopServices.Api.Author.RabbitHandler;

public class EmailEventHandler : IEventHandler<EmailEventQueue>
{
    //private readonly ILogger<EmailEventHandler> _logger;

    public EmailEventHandler()
    {
    }

    //public EmailEventHandler(ILogger<EmailEventHandler> logger)
    //{
    //    _logger = logger;
    //}

    public Task Handle(EmailEventQueue myEvent)
    {
        //_logger.LogInformation($"This is the value from rabbitmq {myEvent.Title}");
        
        return Task.CompletedTask;
    }
}
