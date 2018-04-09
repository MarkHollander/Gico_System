using Gico.CQRS.Model.Implements;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.SystemCommands
{
    public class LocaleStringResourceAddCommand : Command
    {
        public string Id { get; set; }
        public string LanguageId { get; set; }
        public string ResourceName { get; set; }
        public string ResourceValue { get; set; }

    }

    public class LocaleStringResourceChangeCommand : LocaleStringResourceAddCommand
    {

    }
}
