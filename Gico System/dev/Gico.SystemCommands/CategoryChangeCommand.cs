using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.SystemCommands
{
    public class CategoryChangeCommand:CategoryAddCommand
    {
        public CategoryChangeCommand()
        {
        }

        public CategoryChangeCommand(int version) : base(version)
        {
        }

        public string Id { get; set; }
    }
}
