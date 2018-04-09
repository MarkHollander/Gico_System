using Gico.Config;
using Gico.CQRS.Model.Implements;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.SystemCommands.PageBuilder
{
    public class TemplateAddCommand : Command
    {
        public TemplateAddCommand()
        {
        }
        public string Code { get; set; }
        public string TemplateName { get; set; }
        public string Thumbnail { get; set; }
        public string Structure { get; set; }
        public string PathToView { get; set; }
        public EnumDefine.CommonStatusEnum Status { get; set; }
        public EnumDefine.TemplatePageTypeEnum PageType { get; set; }
        public string PageParameters { get; set; }
        public string CreatedUid { get; set; }
    }
}
