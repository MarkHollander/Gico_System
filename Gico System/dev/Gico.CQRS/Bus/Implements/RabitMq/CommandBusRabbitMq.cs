using System;
using System.Collections.Concurrent;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using Gico.Config;
using Gico.CQRS.Bus.Interfaces;
using Gico.CQRS.Bus.Interfaces.RabitMq;
using Gico.CQRS.Model.Implements;

namespace Gico.CQRS.Bus.Implements.RabitMq
{
    public class CommandBusRabbitMq : ICommandBus, IDisposable
    {
        private static string _brokerName;
        private static string _routingKey;
        private static string _queueName;
        private readonly IRabbitMqPersistentConnection _persistentConnection;
        private readonly ILogger<CommandBusRabbitMq> _logger;
        private const int MaxConsumerQueue = 1000000;
        private readonly ConcurrentDictionary<string, EventWaitHandle> _eventWaitHandles;
        private readonly ConcurrentDictionary<string, object> _results;
        private static string _resultBrokerName;
        private static string _resultQueueName;

        private CommandBusRabbitMq()
        {
            _eventWaitHandles = new ConcurrentDictionary<string, EventWaitHandle>();
            _results = new ConcurrentDictionary<string, object>();
            _isDefineResultQueues = new ConcurrentDictionary<string, bool>();

            string environment = ConfigSettingEnum.RabitMqEnvironment.GetConfig();
            _brokerName = $"{ConfigSettingEnum.RabitMqCommandExchangeName.GetConfig()}{environment}";
            _routingKey = $"{ConfigSettingEnum.RabitMqCommandRoutingKey.GetConfig()}{environment}";
            _queueName = $"{ConfigSettingEnum.RabitMqCommandQueueName.GetConfig()}{environment}";
            _resultBrokerName = $"{_brokerName}-{ConfigSettingEnum.InstanceName.GetConfig()}{environment}";
            _resultQueueName = $"{_queueName}-{ConfigSettingEnum.InstanceName.GetConfig()}{environment}";
            
        }

        public CommandBusRabbitMq(IRabbitMqPersistentConnection persistentConnection, ILogger<CommandBusRabbitMq> logger) : this()
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

        public void PublishResult(Message message)
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
                channel.ExchangeDeclare(exchange: message.ResultBrokerName, type: ExchangeType.Direct, durable: false);
                DefineCommandResultQueue(message.ResultBrokerName, message.ResultQueueName);
                var body = message.ToMessage();
                policy.Execute(() =>
                {
                    channel.BasicPublish(exchange: message.ResultBrokerName,
                                     routingKey: _routingKey,
                                     basicProperties: null,
                                     body: body);
                });
            }
        }

        public IModel CreateConsumerChannel(Func<Message, Task<bool>> action)
        {
            try
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
                    _logger.LogInformation("Received message from command queue");
                    Message message;
                    try
                    {
                        message = Message.CreateMessageFromQueue(ea.Body);
                    }
                    catch (Exception e)
                    {
                        e.Data["CommandBusRabbitMq.CreateConsumerChannel.Received.Message"] = "CreateMessageFromQueue Exception";
                        throw;
                    }
                    try
                    {
                        await action(message);
                        channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(e, "Process command Exception");
                        throw;
                    }

                };
                channel.BasicConsume(queue: _queueName, autoAck: false, consumer: consumer);
                channel.CallbackException += (sender, ea) =>
                {
                    _logger.LogInformation("Consumer command queue CallbackException");
                    channel.Dispose();
                    CreateConsumerChannel(action);
                };
                return channel;
            }
            catch (Exception e)
            {
                e.Data["CommandBusRabbitMq.CreateConsumerChannel.Message"] = "CreateConsumerChannel Exception";
                throw;
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

        private readonly ConcurrentDictionary<string, bool> _isDefineResultQueues;

        private void DefineCommandResultQueue(string brokerName, string queueName)
        {
            if (_isDefineResultQueues.ContainsKey(queueName))
            {
                return;
            }
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }
            var channel = _persistentConnection.CreateModel();
            channel.QueueDeclare(queueName, false, false, false, null);
            channel.QueueBind(queue: queueName, exchange: brokerName, routingKey: _routingKey);
            _isDefineResultQueues.TryAdd(queueName, true);
        }

        public IModel CreateConsumerResultChannel()
        {
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }
            var channel = _persistentConnection.CreateModel();
            channel.ExchangeDeclare(exchange: _resultBrokerName, type: ExchangeType.Direct, durable: false);

            DefineCommandResultQueue(_resultBrokerName, _resultQueueName);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
           {
               _logger.LogInformation("Received message from command result queue");
               var message = Message.CreateMessageFromQueue(ea.Body);
               string resultKey = message.ResultKey;
               if (_results.TryAdd(resultKey, message))
               {
                   if (_eventWaitHandles.ContainsKey(resultKey))
                   {
                       _eventWaitHandles[resultKey].Set();
                   }
               }
               else
               {
                   _logger.LogError("Not add command result");
               }
           };
            channel.BasicConsume(queue: _resultQueueName, autoAck: true, consumer: consumer);
            channel.CallbackException += (sender, ea) =>
            {
                _logger.LogInformation("Consumer command result queue CallbackException");
                channel.Dispose();
                CreateConsumerResultChannel();
            };

            return channel;
        }

        private async Task Send(Message message)
        {
            await Task.Factory.StartNew(() =>
            {
                Publish(message);
            });
        }

        public Task<Message> Receive(MessageTypeEnum messageType, TimeSpan timeLock)
        {
            throw new NotImplementedException();
        }

        public async Task<string> Send(Message message, bool hasReceiveResult)
        {
            if (hasReceiveResult)
            {
                if (_eventWaitHandles.Count > MaxConsumerQueue)
                {
                    throw new Exception("EventWaitHandles max queue length");
                }
                string resultKey = Common.Common.GenerateGuid();
                if (_eventWaitHandles.TryAdd(resultKey, new AutoResetEvent(false)))
                {
                    message.ResultKey = resultKey;
                    message.ResultBrokerName = _resultBrokerName;
                    message.ResultQueueName = _resultQueueName;
                    await Send(message);
                    return resultKey;
                }
                else
                {
                    _logger.LogError("Not add EventWaitHandles");
                    throw new Exception("Not add EventWaitHandles");
                }
                //_results[resultKey] = null;                
            }
            else
            {
                await Send(message);
                return string.Empty;
            }
        }

        public async Task SendResult(Message message)
        {
            await Task.Factory.StartNew(() =>
            {
                PublishResult(message);
            });
        }

        public async Task<object> ReceiveCommandResult(string resultKey)
        {
            try
            {
                while (true)
                {
                    object result = null;
                    if (_results.ContainsKey(resultKey))
                    {
                        result = _results[resultKey];
                    }
                    if (result == null)
                    {
                        var eventWaitHandle = _eventWaitHandles[resultKey];
                        bool waitSucess = eventWaitHandle.WaitOne(ConfigSetting.ReceiveCommandTimeout);
                        if (!waitSucess)
                        {
                            throw new TimeoutException();
                        }
                    }
                    else
                    {
                        Message message = (Message)result;
                        return await Task.FromResult(message.Body);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                _eventWaitHandles[resultKey].Dispose();
                _eventWaitHandles.TryRemove(resultKey, out EventWaitHandle e);
                _results.TryRemove(resultKey, out object r);
            }
        }

        public void Dispose()
        {
            _persistentConnection?.Dispose();
        }

    }
}
