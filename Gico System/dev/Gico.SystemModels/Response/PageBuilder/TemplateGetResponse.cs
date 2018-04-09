using Gico.Config;
using Gico.Models.Response;
using Gico.SystemModels.Models.PageBuilder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.SystemModels.Response.PageBuilder
{
    public class TemplateGetResponse : BaseResponse
    {
        public TemplateGetResponse()
        {
            Statuses = KeyValueTypeIntModel.FromEnum(typeof(EnumDefine.CommonStatusEnum));
            PageTypes = KeyValueTypeIntModel.FromEnum(typeof(EnumDefine.TemplatePageTypeEnum));            
        }
        public TemplateViewModel Template { get; set; }
        public KeyValueTypeIntModel[] Statuses { get; private set; }
        public KeyValueTypeIntModel[] PageTypes { get; private set; }        
    }
}
