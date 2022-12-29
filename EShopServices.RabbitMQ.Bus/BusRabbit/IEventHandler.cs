using EShopServices.RabbitMQ.Bus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopServices.RabbitMQ.Bus.BusRabbit;

public interface IEventHandler<in TEvent> : IEventHandler where TEvent : Event
{
    Task Handle(TEvent @myEvent);
}

public interface IEventHandler
{

}
