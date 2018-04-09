using Gico.Config;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.SystemCommands.PageBuilder
{
    public class TemplateChangeCommand : TemplateAddCommand
    {
        public TemplateChangeCommand()
        {
        }

        public string Id { get; set; }

        public DateTime UpdatedDateUtc => this.CreatedDateUtc;

        public string UpdatedUid { get; set; }
    }
}
