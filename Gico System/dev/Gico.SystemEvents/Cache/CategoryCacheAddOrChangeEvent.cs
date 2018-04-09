using Gico.CQRS.Model.Implements;

namespace Gico.SystemEvents.Cache
{
    public class CategoryCacheAddOrChangeEvent:Event
    {

        public string Id { get; set; }

        public string Name { get; set; }

        public string ParentId { get; set; }

        public string Description { get; set; }

        public int DisplayOrder { get; set; }

        public int Version { get; set; }

        public string LanguageId { get; set; }

        public long Status { get; set; }

        public string Code { get; set; }
  
    
    
    }
}
