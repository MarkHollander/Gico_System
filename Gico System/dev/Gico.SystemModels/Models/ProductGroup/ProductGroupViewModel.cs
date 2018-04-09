using System;
using System.Collections.Generic;
using System.Text;
using Gico.Config;

namespace Gico.SystemModels.Models.ProductGroup
{
    public class ProductGroupViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public EnumDefine.CommonStatusEnum Status { get; set; }
        public string Description { get; set; }
        public string Conditions { get; set; }

        public string StatusName => Status.ToString();
    }
}
