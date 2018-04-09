using Gico.Config;
using Gico.Models.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.SystemModels.Request.PageBuilder
{
    public class TemplateConfigSearchRequest : BaseRequest
    {
        public string Id { get; set; }
        public string TemplateId { get; set; }
        public EnumDefine.CommonStatusEnum Status { get; set; }
        public EnumDefine.TemplateConfigComponentTypeEnum ComponentType { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
