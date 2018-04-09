using Gico.CQRS.Model.Implements;

namespace Gico.SystemEvents.Cache
{
    public class LanguageCacheRemoveEvent : Event
    {
        public string Id { get; set; }
    }
}