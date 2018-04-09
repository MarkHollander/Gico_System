using System;
using Gico.CQRS.Model.Implements;

namespace Gico.Events
{
    public class BaseEvent : Event
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string LanguageId { get; set; }
        public string StoreId { get; set; }
        public DateTime CreatedDateUtc { get; set; }
        public string CreatedUid { get; set; }
        public DateTime UpdatedDateUtc { get; set; }
        public string UpdatedUid { get; set; }
        public int Status { get; set; }
        public int ShardId { get; set; }
    }
    public class BaseVersionedEvent : VersionedEvent
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string LanguageId { get; set; }
        public string StoreId { get; set; }
        public DateTime CreatedDateUtc { get; set; }
        public string CreatedUid { get; set; }
        public DateTime UpdatedDateUtc { get; set; }
        public string UpdatedUid { get; set; }
        public int Status { get; set; }
        public int ShardId { get; set; }
    }
}
