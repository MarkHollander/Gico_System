using Gico.Config;
using Gico.CQRS.Model.Implements;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.SystemEvents.Cache
{
    public class ProvinceCacheAddOrChangeEvent : Event
    {
        public string Id { get; set; }
        public string Prefix { get; set; }
        public string ProvinceName { get; set; }
        public string ProvinceNameEN { get; set; }
        public new EnumDefine.CommonStatusEnum Status { get; set; }
        public string ShortName { get; set; }
        public int Priority { get; set; }
        public float? Latitude { get; set; }
        public float? Longitude { get; set; }
        public int RegionId { get; set; }
        public DateTime? UpdatedDateUtc { get; set; }
        public DateTime CreatedDateUtc { get; set; }
        public string CreatedUid { get; set; }
        public string UpdatedUid { get; set; }
    }
}
