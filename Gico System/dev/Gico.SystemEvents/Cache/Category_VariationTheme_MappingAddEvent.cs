using Gico.CQRS.Model.Implements;

namespace Gico.SystemEvents.Cache
{
    public class Category_VariationTheme_MappingAddEvent: Event
    {
        public int[] VariationThemeId { get; set; }

        public string CategoryId { get; set; }
    }
}
