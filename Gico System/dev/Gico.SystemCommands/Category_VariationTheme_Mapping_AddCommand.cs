
using Gico.Config;

namespace Gico.SystemCommands
{
    public class Category_VariationTheme_Mapping_AddCommand : CQRS.Model.Implements.Command
    {
        public int[] VariationThemeId { get; set; }

        public string CategoryId { get; set; }


        public Category_VariationTheme_Mapping_AddCommand()
        {

        }
    }
}
