using Gico.CQRS.Model.Implements;

namespace Gico.SystemEvents.Cache
{
    public class ManufacturerCategoryCacheRemoveEvent:Event
    {
        public int ManufacturerId { get; set; }

        public string CategoryId { get; set; }
    }
}
