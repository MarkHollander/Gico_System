using System;
using Gico.Config;
using Gico.CQRS.Model.Implements;

namespace Gico.SystemCommands
{
    public class VendorAddCommand : Command
    {
        public VendorAddCommand()
        {
        }

        public VendorAddCommand(int version) : base(version)
        {
        }

        public string Email { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Website { get; set; }

        public EnumDefine.TypeEnum Type { get; set; }
        public EnumDefine.StatusEnum Status { get; set; }
        public string Code { get; set; }
        public string CreatedUid { get; set; }
    }
    public class VendorChangeCommand : VendorAddCommand
    {
        public VendorChangeCommand()
        {
        }

        public VendorChangeCommand(int version) : base(version)
        {
        }

        public string Id { get; set; }

        public DateTime UpdatedDateUtc { get; set; }//=> this.CreatedDateUtc;

        public string UpdatedUid { get; set; }
    }
}