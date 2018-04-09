
namespace Gico.SystemEvents.Search
{
    public class LanguageSearchEvent
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Culture { get; set; }
        public string UniqueSeoCode { get; set; }
        public string FlagImageFileName { get; set; }
        public bool Rtl { get; set; }
        public bool LimitedToStores { get; set; }
        public int DefaultCurrencyId { get; set; }
        public bool Published { get; set; }
        public int DisplayOrder { get; set; }
    }
}
