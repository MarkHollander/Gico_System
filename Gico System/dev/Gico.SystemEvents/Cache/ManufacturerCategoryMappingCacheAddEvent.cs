using Gico.CQRS.Model.Implements;

namespace Gico.SystemEvents.Cache
{
    public class ManufacturerCategoryMappingCacheAddEvent : Event
    {
        public int ManufacturerId { get; set; }

        public string CategoryId { get; set; }
    }
}
