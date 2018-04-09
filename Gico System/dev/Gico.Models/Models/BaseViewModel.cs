using System;

namespace Gico.Models.Models
{
    public class BaseViewModel
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string LanguageId { get; set; }
        public string StoreId { get; set; }
        public DateTime CreatedDateUtc { get; set; }
        public string CreatedUid { get; set; }
        public DateTime UpdatedDateUtc { get; set; }
        public string UpdatedUid { get; set; }
        public long Status { get; set; }
        public int ShardId { get; set; }
        public int Version { get; set; }
        public bool IsPublish => Status == 1;
    }
}