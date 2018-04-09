using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.SystemModels.Request
{
    public class LocationRemoveRequest
    {
        public string ProvinceId { get; set; }
        public string DistricId { get; set; }
        public string WardId { get; set; }
        public string StreetId { get; set; }
        public int TypeLocation { get; set; }
    }
}
