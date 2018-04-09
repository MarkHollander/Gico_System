using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using Polly;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using ExchangeType = RabbitMQ.Client.ExchangeType;
using IModel = RabbitMQ.Client.IModel;
using IModelExensions = RabbitMQ.Client.IModelExensions;

namespace Nop.EventNotify
{
    public class EventBusRabbitMq : IEventBus, IDisposable
    {
        private static string _brokerName;
        private static string _routingKey;
        private static string _queueName;
        private readonly IRabbitMqPersistentConnection _persistentConnection;

        public EventBusRabbitMq()
        {
            _brokerName = "RabitMqEventExchangeName";
            _routingKey = "RabitMqEventRoutingKey";
            _queueName = "RabitMqEventQueueName";
        }

        public EventBusRabbitMq(IRabbitMqPersistentConnection persistentConnection) : this()
        {
            _persistentConnection = persistentConnection ?? throw new ArgumentNullException(nameof(persistentConnection));
        }

        public void Publish(Message message)
        {
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }

            var policy = Policy.Handle<BrokerUnreachableException>()
                .Or<SocketException>()
                .WaitAndRetry(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
                {
                    //todo : log
                    // Console.WriteLine(ex.ToString());
                });

            using (var channel = _persistentConnection.CreateModel())
            {
                IModelExensions.ExchangeDeclare(channel, exchange: _brokerName, type: ExchangeType.Direct, durable: true);
                DefineCommandQueue();
                var body = message.ToMessage();
                policy.Execute(() =>
                {
                    channel.BasicPublish(exchange: _brokerName,
                        routingKey: _routingKey,
                        mandatory: false,
                        basicProperties: null,
                        body: body);
                });
            }
        }

        private bool _isDefineQueue = false;

        private void DefineCommandQueue()
        {
            if (_isDefineQueue)
            {
                return;
            }
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }
            var channel = _persistentConnection.CreateModel();
            channel.QueueDeclare(_queueName,true, false, false, null);
            IModelExensions.QueueBind(channel, queue: _queueName, exchange: _brokerName, routingKey: _routingKey);
            _isDefineQueue = true;
        }

        public async Task Notify(Message message)
        {
            await Task.Factory.StartNew(() =>
            {
                Publish(message);
            });
        }

        public void Dispose()
        {
            _persistentConnection?.Dispose();
        }
    }
}