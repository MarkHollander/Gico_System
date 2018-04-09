using Gico.Config;
using Gico.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.SystemModels.Models.PageBuilder
{
    public class TemplateViewModel : BaseViewModel
    {
        public string TemplateName { get; set; }
        public string Thumbnail { get; set; }
        public string Structure { get; set; }
        public string PathToView { get; set; }
        public new EnumDefine.CommonStatusEnum Status { get; set; }
        public EnumDefine.TemplatePageTypeEnum? PageType { get; set; }
        public string PageParameters { get; set; }
        public string StatusName => Status.ToString();
        public string PageTypeName => PageType.ToString();
    }
}
