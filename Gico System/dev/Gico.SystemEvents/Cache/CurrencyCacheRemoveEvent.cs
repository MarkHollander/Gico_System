using Gico.CQRS.Model.Implements;

namespace Gico.SystemEvents.Cache
{
    public class CurrencyCacheRemoveEvent : Event
    {
        public string Id { get; set; }
    }
}