using Gico.Config;
using Gico.Models.Response;
using Gico.SystemModels.Models.PageBuilder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.SystemModels.Response.PageBuilder
{
    public class TemplateConfigSearchResponse : BaseResponse
    {
        public TemplateConfigSearchResponse()
        {
            Statuses = KeyValueTypeIntModel.FromEnum(typeof(EnumDefine.CommonStatusEnum));
            ComponentTypes = KeyValueTypeIntModel.FromEnum(typeof(EnumDefine.TemplateConfigComponentTypeEnum));
            Template = new TemplateViewModel();            
        }
        public int TotalRow { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public TemplateConfigViewModel[] TemplateConfigs { get; set; }        
        public TemplateViewModel Template { get; set; }
        public KeyValueTypeIntModel[] Statuses { get; private set; }
        public KeyValueTypeIntModel[] ComponentTypes { get; private set; }
    }
}
