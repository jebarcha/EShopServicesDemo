using EShopServices.Messenger.Email.SendGridLibrary.Interface;
using EShopServices.Messenger.Email.SendGridLibrary.Model;
using EShopServices.RabbitMQ.Bus.BusRabbit;
using EShopServices.RabbitMQ.Bus.EventQueue;
using System.Runtime.CompilerServices;

namespace EShopServices.Api.Author.RabbitHandler;

public class EmailEventHandler : IEventHandler<EmailEventQueue>
{
    private readonly ILogger<EmailEventHandler> _logger;
    private readonly IMySendGrid _mySendGrid;
    private readonly Microsoft.Extensions.Configuration.IConfiguration _configuration;

    public EmailEventHandler()
    {
    }

    public EmailEventHandler(ILogger<EmailEventHandler> logger, IMySendGrid mySendGrid, Microsoft.Extensions.Configuration.IConfiguration configuration)
    {
        _logger = logger;
        _mySendGrid = mySendGrid;
        _configuration = configuration;
    }

    public async Task Handle(EmailEventQueue myEvent)
    {
        _logger.LogInformation($"This is the value from rabbitmq: {myEvent.Subject}");

        var objData = new SendGridData();
        objData.Content = myEvent.Content;
        objData.EmailDestination = myEvent.Destination;
        objData.NameDestination = myEvent.Destination;
        objData.Subject = myEvent.Subject;
        objData.SendGridAPIKey = _configuration["SendGrid:APIKey"];

        var result = await _mySendGrid.SendEmail(objData);

        if (result.result)
        {
            await Task.CompletedTask;
            return;
        }
    }
}
