using System;
using Gico.Config;
using Gico.CQRS.Model.Implements;

namespace Gico.EmailOrSmsCommands
{
    public class EmailOrSmsBaseCommand : Command
    {
        public EmailOrSmsBaseCommand()
        {

        }

        public EmailOrSmsBaseCommand(EnumDefine.EmailOrSmsTypeEnum type, EnumDefine.EmailOrSmsMessageTypeEnum messageType, string phoneNumber, string email)
        {
            Type = type;
            MessageType = messageType;
            PhoneNumber = phoneNumber;
            Email = email;
        }
        public EnumDefine.EmailOrSmsTypeEnum Type { get; set; }
        public EnumDefine.EmailOrSmsMessageTypeEnum MessageType { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public VerifyAddCommand VerifyAddCommand { get; set; }
        public object Model { get; set; }
    }
}
