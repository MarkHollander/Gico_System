namespace Gico.SystemEvents.Search
{
    public class LocaleStringResourceSearchEvent
    {
        public int LanguageId { get; set; }
        public string ResourceName { get; set; }
        public string ResourceValue { get; set; }
        public string MenuId { get; set; }
    }
}