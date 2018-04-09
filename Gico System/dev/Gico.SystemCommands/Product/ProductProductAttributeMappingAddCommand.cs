using Gico.CQRS.Model.Implements;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.SystemCommands.Product
{
    public class ProductProductAttributeMappingAddCommand : Command
    {
        public ProductProductAttributeMappingAddCommand()
        {
        }
        public string ProductId { get; set; }
        public int AttributeId { get; set; }
        public int AttributeValueId { get; set; }
        public int DisplayUnitId { get; set; }
        public string StringValue { get; set; }
        public bool IsSpecificAttribute { get; set; }
        public bool IsRequired { get; set; }
        public int AttributeType { get; set; }
        public int DisplayOrder { get; set; }
    }
}
