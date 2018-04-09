using Gico.Config;
using Gico.Models.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.SystemModels.Request.PageBuilder
{
    public class TemplateSearchRequest : BaseRequest
    {
        public string Code { get; set; }
        public string TemplateName { get; set; }
        public string PageParameters { get; set; }
        public EnumDefine.CommonStatusEnum Status { get; set; }
        public EnumDefine.TemplatePageTypeEnum PageType { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
