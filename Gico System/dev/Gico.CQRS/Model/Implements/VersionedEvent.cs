using Gico.CQRS.Model.Interfaces;

namespace Gico.CQRS.Model.Implements
{
    public abstract class VersionedEvent : IVersionedEvent
    {
        public VersionedEvent()
        {
            EventId = Common.Common.GenerateGuid();
        }
        public virtual string EventId { get; }
        public virtual int Version { get; set; }
    }
}