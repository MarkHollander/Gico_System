using Gico.CQRS.Model.Implements;

namespace Gico.SystemEvents.Search
{
    public class CurrencySearchEvent : Event
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Rate { get; set; }
        public string DisplayLocale { get; set; }
        public string CustomFormatting { get; set; }
        public bool LimitedToStores { get; set; }
        public bool Published { get; set; }
        public int DisplayOrder { get; set; }
        public int RoundingTypeId { get; set; }
    }
}