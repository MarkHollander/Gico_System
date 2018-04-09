using Gico.Config;
using Gico.CQRS.Model.Implements;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.SystemCommands.PageBuilder
{
    public class TemplateConfigAddCommand : Command
    {
        public TemplateConfigAddCommand()
        {
        }
        public string TemplateId { get; set; }
        public string TemplatePositionCode { get; set; }
        public EnumDefine.TemplateConfigComponentTypeEnum ComponentType { get; set; }
        public string ComponentId { get; set; }
        public string PathToView { get; set; }
        public new EnumDefine.CommonStatusEnum Status { get; set; }
        public string DataSource { get; set; }                
        public string CreatedUid { get; set; }
    }
}
