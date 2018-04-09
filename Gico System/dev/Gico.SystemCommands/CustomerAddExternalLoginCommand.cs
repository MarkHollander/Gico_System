using System;
using System.Collections.Generic;
using System.Text;
using Gico.Config;
using Gico.CQRS.Model.Implements;

namespace Gico.SystemCommands
{
    public class CustomerExternalLoginAddCommand : Command
    {
        public EnumDefine.CutomerExternalLoginProviderEnum LoginProvider { get; set; }
        public string ProviderKey { get; set; }
        public string ProviderDisplayName { get; set; }
        public string CustomerId { get; set; }
        public string Info { get; set; }
        public string VerifyId { get; set; }
    }
}
