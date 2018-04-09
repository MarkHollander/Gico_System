using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.SystemCommands.ProductGroup
{
    public class ProductGroupChangeCommand : ProductGroupAddCommand
    {
        public ProductGroupChangeCommand()
        {
        }

        public string Id { get; set; }

        public DateTime UpdatedDateUtc => this.CreatedDateUtc;

        public string UpdatedUid { get; set; }
    }
}
