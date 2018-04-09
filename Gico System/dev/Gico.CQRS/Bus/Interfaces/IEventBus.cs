using System;
using System.Threading.Tasks;
using Gico.CQRS.Model.Implements;
using RabbitMQ.Client;

namespace Gico.CQRS.Bus.Interfaces
{
    public interface IEventBus
    {
        Task Notify(Message message);
        Task<IModel> CreateConsumerChannel(Func<Message, Task<bool>> action);
    }
}