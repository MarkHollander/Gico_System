using Gico.CQRS.Model.Implements;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.SystemCommands.Product
{
    public class ProductManufacturerMappingAddCommand : Command
    {
        public ProductManufacturerMappingAddCommand()
        {
        }

        public string ProductId { get; set; }
        public int ManufacturerId { get; set; }
    }
}
