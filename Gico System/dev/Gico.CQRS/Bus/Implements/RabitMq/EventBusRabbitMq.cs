using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using Gico.Config;
using Gico.CQRS.Bus.Interfaces;
using Gico.CQRS.Bus.Interfaces.RabitMq;
using Gico.CQRS.Model.Implements;
using Microsoft.Extensions.Logging;
using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using RabbitMQ.Client.Framing;

namespace Gico.CQRS.Bus.Implements.RabitMq
{
    public class EventBusRabbitMq : IEventBus, IDisposable
    {
        private static string _brokerName;
        private static string _routingKey;
        private static string _queueName;
        private readonly IRabbitMqPersistentConnection _persistentConnection;
        private readonly ILogger<EventBusRabbitMq> _logger;

        public EventBusRabbitMq()
        {
            string environment = ConfigSettingEnum.RabitMqEnvironment.GetConfig();
            _brokerName = $"{ConfigSettingEnum.RabitMqEventExchangeName.GetConfig()}{environment}";
            _routingKey = $"{ConfigSettingEnum.RabitMqEventRoutingKey.GetConfig()}{environment}";
            _queueName = $"{ConfigSettingEnum.RabitMqEventQueueName.GetConfig()}{environment}";
        }

        public EventBusRabbitMq(IRabbitMqPersistentConnection persistentConnection, ILogger<EventBusRabbitMq> logger) : this()
        {
            _persistentConnection = persistentConnection ?? throw new ArgumentNullException(nameof(persistentConnection));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
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
                    _logger.LogWarning(ex.ToString());
                });

            using (var channel = _persistentConnection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: _brokerName, type: ExchangeType.Direct, durable: true);
                DefineCommandQueue();
                var body = message.ToMessage();
                policy.Execute(() =>
                {
                    channel.BasicPublish(exchange: _brokerName,
                        routingKey: _routingKey,
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
            channel.QueueDeclare(_queueName, true, false, false, null);
            channel.QueueBind(queue: _queueName, exchange: _brokerName, routingKey: _routingKey);
            _isDefineQueue = true;
        }

        public async Task<IModel> CreateConsumerChannel(Func<Message, Task<bool>> action)
        {
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }
            var channel = _persistentConnection.CreateModel();
            channel.ExchangeDeclare(exchange: _brokerName, type: ExchangeType.Direct, durable: true);
            DefineCommandQueue();

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, ea) =>
            {
                _logger.LogInformation($"Received event {ea.DeliveryTag}");
                //var eventName1 = ea.RoutingKey;
                var message = Message.CreateMessageFromQueue(ea.Body);
                await action(message);
                channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            };
            channel.BasicConsume(queue: _queueName, autoAck: false, consumer: consumer);
            channel.CallbackException += async (sender, ea) =>
            {
                channel.Dispose();
                await CreateConsumerChannel(action);
            };

            return await Task.FromResult(channel);
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