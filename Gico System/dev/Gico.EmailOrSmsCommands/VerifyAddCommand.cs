using System;
using Gico.Config;
using Gico.CQRS.Model.Implements;

namespace Gico.EmailOrSmsCommands
{
    public class VerifyAddCommand : Command
    {
        public VerifyAddCommand()
        {

        }

        public VerifyAddCommand(TimeSpan expireDate, EnumDefine.VerifyTypeEnum type, object model)
        {
            ExpireDate = expireDate;
            Type = type;
            Model = model;
        }
        public TimeSpan ExpireDate { get; set; }
        public EnumDefine.VerifyTypeEnum Type { get; set; }
        public object Model { get; set; }
    }
}