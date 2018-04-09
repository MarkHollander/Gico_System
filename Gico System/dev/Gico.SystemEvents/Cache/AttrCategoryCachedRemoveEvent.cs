using Gico.CQRS.Model.Implements;

namespace Gico.SystemEvents.Cache
{
    public class AttrCategoryCachedRemoveEvent : Event
    {
        public int AttributeId { get; set; }

        public string CategoryId { get; set; }
    }
}
