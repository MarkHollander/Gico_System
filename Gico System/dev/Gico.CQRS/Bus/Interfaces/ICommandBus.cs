using System;
using System.Threading.Tasks;
using Gico.CQRS.Model.Implements;
using RabbitMQ.Client;

namespace Gico.CQRS.Bus.Interfaces
{
    public interface ICommandBus
    {
        Task<string> Send(Message obj, bool hasReceiveResult);

        Task SendResult(Message message);

        Task<object> ReceiveCommandResult(string resultKey);

        Task<Message> Receive(MessageTypeEnum messageType, TimeSpan timeLock);

        IModel CreateConsumerChannel(Func<Message, Task<bool>> action);
        IModel CreateConsumerResultChannel();
    }
}