using Gico.CQRS.Model.Implements;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.SystemEvents.Cache
{
    public class Category_VariationTheme_MappingRemoveEvent : Event
    {
        public int VariationThemeId { get; set; }

        public string CategoryId { get; set; }
    }
}
