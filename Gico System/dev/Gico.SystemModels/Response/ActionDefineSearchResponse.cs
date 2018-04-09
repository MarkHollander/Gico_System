using System.Collections.Generic;
using Gico.Config;
using Gico.Models.Response;

namespace Gico.SystemModels.Response
{
    public class ActionDefineSearchResponse : BaseResponse
    {
        public KeyValuePair<string, ActionDefineViewModel[]>[] ActionDefines { get; set; }
        public int TotalRow { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}