using Gico.Models.Request;

namespace Gico.SystemModels.Request
{
    public class LanguageSearchRequest : BaseRequest
    {
        public string Name { get;  set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}