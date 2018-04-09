using Gico.Config;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.SystemModels.Models
{
    public class AttrCategoryModel:BaseModel
    {
        public int AttributeId { get; set; }

        public string CategoryId { get; set; }

        public bool IsFilter { get; set; }
        public string FilterSpan { get; set; }
        public int BaseUnitId { get; set; }

        public EnumDefine.AttrCategoryType AttributeType { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsRequired { get; set; }

        public string AttributeName { get; set; }
    }
}
