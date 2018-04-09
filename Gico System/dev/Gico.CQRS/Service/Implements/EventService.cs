//using System;
//using System.Threading.Tasks;
//using Gico.CQRS.Bus.Interfaces;
//using Gico.CQRS.EventStorage.Interfaces;
//using Gico.CQRS.Model.Implements;
//using Gico.CQRS.Service.Interfaces;

//namespace Gico.CQRS.Service.Implements
//{
//    public class EventService : IEventService
//    {
//        private readonly ICommandBus _bus;
//        private readonly IEventStorageDao _eventStorageDao;
//        private readonly IMessageProcessor _messageProcessor;

//        public EventService(IEventStorageDao eventStorageDao, ICommandBus bus, IMessageProcessor messageProcessor)
//        {
//            _eventStorageDao = eventStorageDao;
//            _bus = bus;
//            _messageProcessor = messageProcessor;
//        }

//        public async Task Add(Event @event)
//        {
//            Message message = new Message(@event);
//            await _eventStorageDao.Add(message);
//        }

//        public async Task Add(Event[] events)
//        {
//            Message[] messages = new Message[events.Length];
//            for (int i = 0; i < events.Length; i++)
//            {
//                var @event = events[i];
//                messages[i] = new Message(@event);
//            }
//            await _eventStorageDao.Add(messages);
//        }

//        public async Task Add(VersionedEvent @event)
//        {
//            Message message = new Message(@event);
//            await _eventStorageDao.Add(message);
//        }

//        public async Task Add(VersionedEvent[] events)
//        {
//            Message[] messages = new Message[events.Length];
//            for (int i = 0; i < events.Length; i++)
//            {
//                var @event = events[i];
//                messages[i] = new Message(@event);
//            }
//            await _eventStorageDao.Add(messages);
//        }

//        public async Task Publish(Event @event)
//        {
//            Message message = new Message(@event);
//            //await _bus.Send(message);
//        }

//        public async Task Publish(Event[] @events)
//        {
//            Message[] messages = new Message[events.Length];
//            for (int i = 0; i < events.Length; i++)
//            {
//                var @event = events[i];
//                messages[i] = new Message(@event);
//            }
//           // await _bus.Send(messages);
//        }

//        public async Task Publish(VersionedEvent @event)
//        {
//            Message message = new Message(@event);
//            //await _bus.Send(message);
//        }

//        public async Task Publish(VersionedEvent[] events)
//        {
//            Message[] messages = new Message[events.Length];
//            for (int i = 0; i < events.Length; i++)
//            {
//                var @event = events[i];
//                messages[i] = new Message(@event);
//            }
//            //await _bus.Send(messages);
//        }

//        public async Task Receive(Message message)
//        {
//            //var data = message.BodyData;
//           // await _messageProcessor.ProcessMessage(data);
//        }

//        public async Task ProcessMessage()
//        {
//            Message message = await _bus.Receive(MessageTypeEnum.Event, TimeSpan.FromMinutes(2));
//            await Receive(message);
//        }
//    }
//}