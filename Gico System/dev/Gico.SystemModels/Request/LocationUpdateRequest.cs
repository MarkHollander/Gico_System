using Gico.Models.Request;
using Gico.SystemModels.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.SystemModels.Request
{
    public class LocationUpdateRequest : BaseRequest
    {
        public string Id { get; set; }
        public string Prefix { get; set; }
        public string ProvinceName { get; set; }
        public string ProvinceNameEN { get; set; }
        public long Status { get; set; }
        public string ShortName { get; set; }
        public int Priority { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public int RegionId { get; set; }
        public DateTime UpdatedDateUtc { get; set; }
        public DateTime CreatedDateUtc { get; set; }
        public string CreatedUid { get; set; }
        public string UpdatedUid { get; set; }
        public string DistrictName { get; set; }
        public string DistrictNameEN { get; set; }
        public string ProvinceId { get; set; }
        public string WardName { get; set; }
        public string WardNameEN { get; set; }
        public string DistrictId { get; set; }
    }
}
