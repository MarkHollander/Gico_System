using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.SystemCommands
{
    public class AttrCategoryRemoveCommand : CQRS.Model.Implements.Command
    {
        public AttrCategoryRemoveCommand()
        {

        }
        public int AttributeId { get; set; }

        public string CategoryId { get; set; }
    }
}
