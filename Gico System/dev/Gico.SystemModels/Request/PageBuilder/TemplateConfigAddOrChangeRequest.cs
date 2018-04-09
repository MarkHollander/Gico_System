using Gico.Config;
using Gico.Models.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.SystemModels.Request.PageBuilder
{
    public class TemplateConfigAddOrChangeRequest : BaseRequest
    {
        public string Id { get; set; }
        public string TemplateId { get; set; }
        public string TemplatePositionCode { get; set; }
        public EnumDefine.TemplateConfigComponentTypeEnum ComponentType { get; set; }
        public string ComponentId { get; set; }
        public string PathToView { get; set; }
        public EnumDefine.CommonStatusEnum Status { get; set; }
        public string DataSource { get; set; }
    }
}
