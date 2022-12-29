using EShopServices.RabbitMQ.Bus.BusRabbit;
using EShopServices.RabbitMQ.Bus.Commands;
using EShopServices.RabbitMQ.Bus.Events;
using MediatR;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace EShopServices.RabbitMQ.Bus.Implement;

public class RabbitEventBus : IRabbitEventBus
{
    private readonly IMediator _mediator;
    private readonly Dictionary<string, List<Type>> _handlers;
    private readonly List<Type> _eventTypes;

    public RabbitEventBus(IMediator mediator)
    {
        _mediator = mediator;
        _handlers = new Dictionary<string, List<Type>>();
        _eventTypes = new List<Type>();
    }

    public void Publish<T>(T myEvent) where T : Event
    {
        var factory = new ConnectionFactory() { HostName = "rabbit-jbc-web" }; //"localhost" };
        using (var connection = factory.CreateConnection())
        {
            using (var channel = connection.CreateModel())
            {
                var eventName = myEvent.GetType().Name;

                channel.QueueDeclare(eventName, false, false, false, null);

                var message = JsonConvert.SerializeObject(myEvent);
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish("", eventName, null, body);
            }
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
        var handlerType = typeof(TH);

        if (!_eventTypes.Contains(typeof(T)))
        {
            _eventTypes.Add(typeof(T));
        }

        if (!_handlers.ContainsKey(eventName))
        {
            _handlers.Add(eventName, new List<Type>());
        }

        if (_handlers[eventName].Any(x => x.GetType() == handlerType))
        {
            throw new ArgumentException($"The handler {handlerType.Name} was previously registered by {eventName}");
        }

        _handlers[eventName].Add(handlerType);

        var factory = new ConnectionFactory()
        {
            HostName = "rabbit-jbc-web", //"localhost",
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
                var subscriptions = _handlers[eventName];
                foreach (var sb in subscriptions) 
                {
                    var handler = Activator.CreateInstance(sb);
                    if (handler is null) continue;

                    var handlerType = _eventTypes.SingleOrDefault(x => x.Name == eventName);
                    var eventDS = JsonConvert.DeserializeObject(message, handlerType);

                    var concreateType = typeof(IEventHandler<>).MakeGenericType(handlerType);

                    await (Task)concreateType.GetMethod("Handle").Invoke(handler, new object[] { eventDS });
                }
            }
        }
        catch (Exception ex)
        {

        }

    }
}
