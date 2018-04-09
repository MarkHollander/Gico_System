using Gico.CQRS.Model.Interfaces;
using Gico.DataObject;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gico.Domains
{
    public class BaseDomain : ShardingDomain
    {
      
        public long NumericalOrder { get; protected set; }
        public string Id { get; protected set; }
        public string Code { get; protected set; }
        public DateTime CreatedDateUtc { get; protected set; }
        public string CreatedUid { get; protected set; }
        public DateTime UpdatedDateUtc { get; protected set; }
        public string UpdatedUid { get; protected set; }
        public string LanguageId { get; protected set; }
        public string StoreId { get; protected set; }
        public long Status { get; protected set; }

        private List<IEvent> _events;
        public List<IEvent> Events => _events;
        public void AddEvent(IEvent @event)
        {
            _events = _events ?? new List<IEvent>();
            _events.Add(@event);
        }
        public void RemoveEvent(IEvent @event)
        {
            if (_events is null) return;
            _events.Remove(@event);
        }
        public void RemoveEvent(string id)
        {
            if (_events is null) return;
            var events = _events.Where(p => p.EventId == id);
            foreach (var @event in events)
            {
                RemoveEvent(@event);
            }
        }
    }
}
