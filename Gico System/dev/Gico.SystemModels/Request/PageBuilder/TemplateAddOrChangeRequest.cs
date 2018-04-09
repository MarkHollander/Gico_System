using Gico.Config;
using Gico.Models.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.SystemModels.Request.PageBuilder
{
    public class TemplateAddOrChangeRequest : BaseRequest
    {
        public string Id { get; set; }
        public string TemplateName { get; set; }
        public string Thumbnail { get; set; }
        public string Structure { get; set; }
        public string PathToView { get; set; }
        public EnumDefine.CommonStatusEnum Status { get; set; }
        public string Code { get; set; }
        public EnumDefine.TemplatePageTypeEnum PageType { get; set; }
        public string PageParameters { get; set; }
    }
}
