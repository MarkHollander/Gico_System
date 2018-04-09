

using Gico.Config;

namespace Gico.SystemCommands
{
    public class AttrCategoryAddCommand : CQRS.Model.Implements.Command
    {
        public AttrCategoryAddCommand()
        {

        }
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
