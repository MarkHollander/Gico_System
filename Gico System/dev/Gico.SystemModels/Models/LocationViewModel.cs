using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.SystemModels.Models
{
    public class LocationViewModel
    {
        public string ProvinceName { get; set; }
        public int Id { get; set; }
    }

    public class DistricsViewModel
    {
        public string DistricName { get; set; }
        public int Id { get; set; }
    }

    public class WardViewModel
    {
        public string WardName { get; set; }
        public int Id { get; set; }
    }

    public class StreetViewModel
    {
        public string StreetName { get; set; }
        public int Id { get; set; }
    }

    public class UpdateProvinceModel : BaseModel
    {
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
    }
}
