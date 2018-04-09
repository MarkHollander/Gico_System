using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gico.CQRS.Bus.Interfaces;
using Gico.CQRS.Model.Implements;
using Gico.CQRS.Model.Interfaces;
using Gico.CQRS.Service.Interfaces;
using Microsoft.Extensions.Logging;

namespace Gico.CQRS.Service.Implements
{
    public class EventSender : IEventSender
    {
        private readonly IEventBus _eventBus;
        private readonly ILogger<EventSender> _logger;
        private readonly List<IEvent> _events;
        public EventSender(IEventBus eventBus, ILogger<EventSender> logger)
        {
            _eventBus = eventBus;
            _logger = logger;
            _events = new List<IEvent>();
        }

        public void Add(IEvent @event)
        {
            if (@event == null)
            {
                return;
            }
            _events.Add(@event);
        }
        public void Add(List<IEvent> events)
        {
            if (events == null || events.Count <= 0)
            {
                return;
            }
            _events.AddRange(events);
        }
        private async Task Notify(IEvent @event)
        {
            if (@event == null)
            {
                return;
            }
            Message message = new Message(@event);
            await _eventBus.Notify(message);
        }
        
        public async Task Notify(List<IEvent> events)
        {
            if (events == null)
            {
                return;
            }
            _events.AddRange(events);
            await Notify();
        }
        public async Task Notify()
        {
            if (_events == null || _events.Count <= 0)
            {
                return;
            }
            try
            {
                Task[] tasks = new Task[_events.Count];
                int i = 0;
                foreach (var @event in _events)
                {
                    tasks[i] = Notify(@event);
                    i++;
                }
                await Task.WhenAll(tasks);
            }
            catch (Exception e)
            {
                e.Data["EventSender.Notify.MessageException"] = "Notify Exception";
                e.Data["EventSender.Notify.Events"] = _events;
                _logger.LogTrace(e, "Notify Exception", _events);
            }

        }
    }
}