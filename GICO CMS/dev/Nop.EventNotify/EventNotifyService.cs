using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Domain.Directory;
using Nop.Core.Domain.Localization;
using Nop.Core.Events;
using Nop.EventNotify.Mapping;

namespace Nop.EventNotify
{
    public class EventNotifyService : IEventNotifyService
    {
        private readonly IEventBus _eventBus;
        private readonly EventLogDao _eventLogDao;

        public EventNotifyService(IEventBus eventBus, EventLogDao eventLogDao)
        {
            _eventBus = eventBus;
            _eventLogDao = eventLogDao;
        }

        public async Task Notify<T>(T @event)
        {
            Message[] messages = await ToMessage(@event);
            if (messages == null || messages.Length <= 0)
            {
                return;
            }
            IList<Task> logErrors = new List<Task>();
            foreach (var message in messages)
            {
                try
                {
                    await _eventBus.Notify(message);
                }
                catch (Exception e)
                {
                    var task = _eventLogDao.AddAsync(message, e);
                    logErrors.Add(task);
                }
            }
            if (logErrors.Count > 0)
            {
                await Task.WhenAll(logErrors);
            }
        }

        private readonly HashSet<string> _objectTypeNames = new HashSet<string>()
        {
            "EntityUpdated`1",
            "EntityInserted`1",
            "EntityDeleted`1"
        };
        private bool ValidateObjectType<T>(T @event)
        {
            var type = @event.GetType();
            string path = @"C:\Workspaces\txt.txt";
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine($"{type.FullName}---{type.Name}");
            }
            return _objectTypeNames.Contains(type.Name);
        }

        private async Task<Message[]> ToMessage(object @event)
        {
            Message[] messages = new Message[0];
            try
            {
                var type = @event.GetType();
                if (type == typeof(EntityUpdated<Currency>))
                {
                    var obj = ((EntityUpdated<Currency>)@event).Entity.ToAddOrChangeEvent();
                    messages = new[]
                    {
                        new Message(obj)
                    };
                }
                if (type == typeof(EntityInserted<Currency>))
                {
                    var obj = ((EntityInserted<Currency>)@event).Entity.ToAddOrChangeEvent();
                    messages = new[]
                    {
                        new Message(obj)
                    };
                }
                if (type == typeof(EntityDeleted<Currency>))
                {
                    var obj = ((EntityDeleted<Currency>)@event).Entity.ToRemoveEvent();
                    messages = new[]
                    {
                        new Message(obj)
                    };
                }

                else if (type == typeof(EntityUpdated<Language>))
                {
                    var obj = ((EntityUpdated<Language>)@event).Entity.ToEvent();
                    messages = new[]
                    {
                        new Message(obj)
                    };
                }
                else if (type == typeof(EntityUpdated<LocaleStringResource>))
                {
                    var obj = ((EntityUpdated<LocaleStringResource>)@event).Entity.ToEvent();
                    messages = new[]
                    {
                        new Message(obj)
                    };
                }

            }
            catch (Exception e)
            {
                await _eventLogDao.AddAsync(-1, e);
            }
            return await Task.FromResult(messages);
        }

    }
}
