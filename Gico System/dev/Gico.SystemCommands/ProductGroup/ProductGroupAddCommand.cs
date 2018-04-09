using Gico.Config;
using Gico.CQRS.Model.Implements;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.SystemCommands.ProductGroup
{
    public class ProductGroupAddCommand : Command
    {
        public ProductGroupAddCommand()
        {
        }
        public string Name { get; set; }
        public EnumDefine.CommonStatusEnum Status { get; set; }
        public string Description { get; set; }
        public string CreatedUid { get; set; }
    }
}
