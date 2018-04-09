using Gico.Config;
using Gico.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Gico.Models.Response;

namespace Gico.SystemModels.Models.PageBuilder
{
    public class TemplateConfigViewModel : BaseViewModel
    {   
        public string TemplateId { get; set; }        
        public string TemplatePositionCode { get; set; }
        public string ComponentId { get; set; }
        public string PathToView { get; set; }
        public new EnumDefine.CommonStatusEnum Status { get; set; }
        public EnumDefine.TemplateConfigComponentTypeEnum? ComponentType { get; set; }
        public string DataSource { get; set; }
        public string StatusName => Status.ToString();
        public string ComponentTypeName => ComponentType.ToString();
        public KeyValueTypeStringModel Component { get; set; }
    }
}
