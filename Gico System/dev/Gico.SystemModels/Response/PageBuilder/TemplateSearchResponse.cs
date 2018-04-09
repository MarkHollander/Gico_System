using Gico.Config;
using Gico.Models.Response;
using Gico.SystemModels.Models.PageBuilder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.SystemModels.Response.PageBuilder
{
    public class TemplateSearchResponse : BaseResponse
    {
        public TemplateSearchResponse()
        {
            Statuses = KeyValueTypeIntModel.FromEnum(typeof(EnumDefine.CommonStatusEnum));
            PageTypes = KeyValueTypeIntModel.FromEnum(typeof(EnumDefine.TemplatePageTypeEnum));
        }
        public int TotalRow { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public TemplateViewModel[] Templates { get; set; }        
        public KeyValueTypeIntModel[] Statuses { get; private set; }
        public KeyValueTypeIntModel[] PageTypes { get; private set; }
    }
}
