using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.SystemCommands
{
    public class ManufacturerCategoryMappingRemoveCommand: CQRS.Model.Implements.Command
    {
        public ManufacturerCategoryMappingRemoveCommand()
        {

        }
        public int ManufacturerId { get; set; }

        public string CategoryId { get; set; }
    }
}
