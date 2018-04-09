using Gico.CQRS.Model.Implements;

namespace Gico.SystemEvents.Cache
{
    public class LocaleStringResourceCacheAddOrChangeEvent : Event
    {
        public string Id { get; set; }
        public string LanguageId { get; set; }
        public string ResourceName { get; set; }
        public string ResourceValue { get; set; }
        public string[] MenuIds { get; set; }
    }
}