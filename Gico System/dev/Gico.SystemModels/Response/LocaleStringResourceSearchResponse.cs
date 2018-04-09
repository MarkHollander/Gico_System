using Gico.Config;
using Gico.Models.Response;

namespace Gico.SystemModels.Response
{
    public class LocaleStringResourceSearchResponse : BaseResponse
    {
        public KeyValueTypeStringModel[] Languages { get; set; }
        public LocaleStringResourceViewModel[] Locales { get; set; }
        public int TotalRow { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        
    }
}