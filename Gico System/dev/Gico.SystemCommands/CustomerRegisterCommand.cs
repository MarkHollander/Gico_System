using System;
using Gico.Config;
using Gico.CQRS.Model.Implements;

namespace Gico.SystemCommands
{
    public class CustomerRegisterCommand : Command
    {
        public CustomerRegisterCommand()
        {
        }

        public CustomerRegisterCommand(int version) : base(version)
        {
        }

        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public long SystemNumericalOrder { get; set; }
        public string Mobile { get; set; }
        public string RegisterIp { get; set; }
        public EnumDefine.GenderEnum Gender { get; set; }
        public DateTime Birthday { get; set; }
    
        
    }
}