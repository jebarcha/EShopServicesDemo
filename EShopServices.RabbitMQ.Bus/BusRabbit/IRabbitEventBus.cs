using EShopServices.RabbitMQ.Bus.Commands;
using EShopServices.RabbitMQ.Bus.Events;

namespace EShopServices.RabbitMQ.Bus.BusRabbit;

public interface IRabbitEventBus
{
    Task SendCommand<T>(T command) where T : Command;

    void Publish<T>(T @MyEvent) where T : Event;

    void Subscribe<T, TH>() where T : Event
                            where TH : IEventHandler<T>;
}
