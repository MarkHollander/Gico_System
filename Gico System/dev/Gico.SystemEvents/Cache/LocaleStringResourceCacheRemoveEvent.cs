using Gico.CQRS.Model.Implements;

namespace Gico.SystemEvents.Cache
{
    public class LocaleStringResourceCacheRemoveEvent : Event
    {
        public string LanguageId { get; set; }
        public string ResourceName { get; set; }
        public string[] MenuIds { get; set; }
    }
}