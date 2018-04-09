using System;
using RabbitMQ.Client;

namespace Gico.CQRS.Bus.Interfaces.RabitMq
{
    public interface IRabbitMqPersistentConnection
        : IDisposable
    {
        bool IsConnected { get; }

        bool TryConnect();

        IModel CreateModel();
    }
}
