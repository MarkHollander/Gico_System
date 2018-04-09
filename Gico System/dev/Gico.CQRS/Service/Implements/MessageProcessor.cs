using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using Gico.CQRS.Service.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Gico.CQRS.Service.Implements
{
    public abstract class MessageProcessor : IMessageProcessor
    {
        private readonly ConcurrentDictionary<Type, IMessageHandler> _handlers;
        protected ServiceProvider ServiceProvider;

        protected MessageProcessor()
        {
            _handlers = new ConcurrentDictionary<Type, IMessageHandler>();

        }
        public void Register(ServiceProvider provider)
        {
            ServiceProvider = provider;
            var services = ServiceProvider.GetServices<IMessageHandler>();
            foreach (var service in services)
            {
                var genericHandler = IsCommonProcess ? typeof(ICommandHandler<,>) : typeof(IEventHandler<>);

                var supportedCommandTypes = service.GetType()
                    .GetInterfaces()
                    .Where(iface => iface.IsGenericType && iface.GetGenericTypeDefinition() == genericHandler)
                    .Select(iface => iface.GetGenericArguments()[0])
                    .ToArray();

                foreach (var commandType in supportedCommandTypes)
                {
                    _handlers.TryAdd(commandType, service);
                }
            }


            //var genericHandler = typeof(IMessageHandler);
            //var supportedCommandTypes = messageHandler.GetType().GetMethods().Where(p => p.Name == "Handle").Select(p => p.GetParameters()).Where(p => p.Length == 1)
            //    .Select(p => p.First().ParameterType).ToArray();

            //if (_handlers.Keys.Any(registeredType => supportedCommandTypes.Contains(registeredType)))
            //    throw new ArgumentException("The command handled by the received handler already has a registered handler.");

            //// Register this handler for each of the handled types.


        }

        public async Task<object> Handle(object payload)
        {
            var commandType = payload.GetType();
            if (_handlers.TryGetValue(commandType, out var handler))
            {
                var service = ServiceProvider.GetService(handler.GetType());
                if (service == null)
                {
                    throw new Exception("Handler not register");
                }
                if (IsCommonProcess)
                {
                    return await ((dynamic)service).Handle((dynamic)payload);
                }
                else
                {
                    await ((dynamic)service).Handle((dynamic)payload);
                    return true;
                }

            }
            return null;
            // There can be a generic logging/tracing/auditing handlers
            //if (_handlers.TryGetValue(typeof(ICommand), out handler))
            //{
            //    ((dynamic)handler).Handle((dynamic)payload);
            //}
        }

        public abstract void Start();
        public abstract bool IsCommonProcess { get; }
    }
}