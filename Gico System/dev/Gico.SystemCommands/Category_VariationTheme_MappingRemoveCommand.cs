using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.SystemCommands
{
    public class Category_VariationTheme_Mapping_RemoveCommand: CQRS.Model.Implements.Command
    { 
        public Category_VariationTheme_Mapping_RemoveCommand()
        {

        }

        public string CategoryId { get; set; }

        public int VariationThemeId { get; set; }
        

    }
}
