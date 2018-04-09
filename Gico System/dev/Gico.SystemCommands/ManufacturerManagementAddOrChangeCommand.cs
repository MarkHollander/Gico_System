using System;
using System.Collections.Generic;
using System.Text;
using Gico.CQRS.Model.Implements;
using Gico.Config;

namespace Gico.SystemCommands
{
    public class ManufacturerManagementAddCommand : Command
    {
        public ManufacturerManagementAddCommand()
        { }
        public ManufacturerManagementAddCommand(int version): base(version)
        { }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
        public DateTime CreateDateUtc { get; set; }
        
    }

    public class ManufacturerManagementChangeCommand : ManufacturerManagementAddCommand
    {
        public ManufacturerManagementChangeCommand() { }

        public ManufacturerManagementChangeCommand(int version) : base(version)
        { }

        public int Id { get; set; }
        public DateTime UpdatedDateUtc { get; set; }//=> this.CreatedDateUtc;        
    }
}
