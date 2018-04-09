using Gico.Config;
using Gico.Models.Response;
using Gico.SystemModels.Models.PageBuilder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.SystemModels.Response.PageBuilder
{
    public class TemplateConfigGetResponse : BaseResponse
    {
        public TemplateConfigGetResponse()
        {
            Statuses = KeyValueTypeIntModel.FromEnum(typeof(EnumDefine.CommonStatusEnum),false);
            ComponentTypes = KeyValueTypeIntModel.FromEnum(typeof(EnumDefine.TemplateConfigComponentTypeEnum), false);
        }
        public TemplateConfigViewModel TemplateConfig { get; set; }
        public TemplateViewModel Template { get; set; }
        public KeyValueTypeIntModel[] Statuses { get; private set; }
        public KeyValueTypeIntModel[] ComponentTypes { get; private set; }
    }
}
