using Gico.CQRS.Model.Implements;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.SystemCommands.Product
{
    public class WarehouseProductMappingChangeCommand : WarehouseProductMappingAddCommand
    {
        public string Id { get; set; }

        public DateTime UpdatedDateUtc => this.CreatedDateUtc;

        public string UpdatedUid { get; set; }
    }
}
