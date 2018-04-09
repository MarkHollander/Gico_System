using Gico.CQRS.Model.Implements;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.SystemCommands.Product
{
    public class ProductCategoryMappingAddCommand : Command
    {
        public ProductCategoryMappingAddCommand()
        {
        }
        public string ProductId { get; set; }
        public string CategoryId { get; set; }
        public bool IsMainCategory { get; set; }
        public int DisplayOrder { get; set; }        
    }
}
