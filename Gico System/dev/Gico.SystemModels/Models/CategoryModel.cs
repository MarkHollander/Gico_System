using System;
using Gico.Config;
using Gico.Models.Models;

namespace Gico.SystemModels.Models
{
    public class CategoryModel: BaseModel
    {
        public string Name { get; set; }
        public string ParentId { get; set; }
        public string Description { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsPublish { get; set; }
        public string Logos { get; set; }

        public new EnumDefine.CategoryStatus Status { get; set; }
    }

    public class CategoryAttrModel
    {
        public bool IsFilter { get; set; }

        public int BaseUnitId { get; set; }

        public int DisplayOrder { get; set; }

        public string AttributeName { get; set; }

        public int AttributeId { get; set; }
        public EnumDefine.AttrCategoryType AttributeType { get; set; }

    }


}
