using Gico.Config;
using Gico.CQRS.Model.Implements;

namespace Gico.SystemEvents.Cache
{
    public class AttrCategoryCachedAddOrChangeEvent : Event
    {
        public int AttributeId { get; set; }

        public string CategoryId { get; set; }

        public bool IsFilter { get; set; }
        public string FilterSpan { get; set; }
        public int BaseUnitId { get; set; }

        public EnumDefine.AttrCategoryType AttributeType { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsRequired { get; set; }
    }
}
