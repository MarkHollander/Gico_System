using System;
using System.Collections.Generic;
using System.Text;
using Gico.Config;

namespace Gico.SystemModels.Request.ProductGroup
{
    public class ProductGroupGetsRequest
    {
        public string Name { get; set; }
        public EnumDefine.CommonStatusEnum Status { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
