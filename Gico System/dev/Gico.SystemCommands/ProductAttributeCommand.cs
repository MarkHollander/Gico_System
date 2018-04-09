using System;
using Gico.Config;
using Gico.CQRS.Model.Implements;

namespace Gico.SystemCommands
{
    public class ProductAttributeCommand : Command
    {
        public ProductAttributeCommand()
        {
        }

        public ProductAttributeCommand(int version) : base(version)
        {

        }

        public string Id { get; set; }
        public string Name { get; set; }
        public EnumDefine.StatusEnum Status { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
        public string CreatedUserId { get; set; }
        public string UpdatedUserId { get; set; }
    }

    public class ProductAttributeValueCommand : Command
    {
        public ProductAttributeValueCommand()
        {
        }

        public ProductAttributeValueCommand(int version) : base(version)
        {

        }

        public string AttributeValueId { get; set; }
        public string AttributeId { get; set; }
        public string Value { get; set; }
        public int UnitId { get; set; }
        public EnumDefine.StatusEnum AttributeValueStatus { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
        public string CreatedUserId { get; set; }
        public string UpdatedUserId { get; set; }
        public int DisplayOrder { get; set; }
    }
}