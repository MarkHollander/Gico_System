namespace Gico.Models.Request
{
    public class BaseRequest
    {
        public BaseRequest()
        {
            LanguageDefaultId = "2";
        }
        public string LanguageId { get; set; }
        public string LanguageDefaultId { get; set; }

        public string LanguageCurrentId => string.IsNullOrEmpty(LanguageId) ? LanguageDefaultId : LanguageId;
    }
}
