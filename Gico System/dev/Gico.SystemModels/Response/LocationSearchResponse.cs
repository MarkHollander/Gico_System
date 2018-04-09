using Gico.Models.Response;
using Gico.SystemModels.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.SystemModels.Response
{
    public class LocationSearchResponse : BaseResponse
    {
        public int TotalRow { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public List<ProvinceResponse> ListLocation { get; set; }
        public LocationViewModel[] LtsProvince { get; set; }
        public DistricsViewModel[] LtsDictrics { get; set; }
        public WardViewModel[] LtsWard { get; set; }
        public StreetViewModel[] LtsStreet { get; set; }
    }

    public class ProvinceResponse
    {
        public string ProvinceName { get; set; }
        public string Region { get; set; }
        public List<DictrictsResponse> ListDistrics { get; set; }
    }

    public class DictrictsResponse : BaseResponse
    {
        public string DistrictName { get; set; }
        public List<WardResponse> ListWard { get; set; }
    }

    public class WardResponse : BaseResponse
    {
        public string WardName { get; set; }
        public List<StreetResponse> ListStreet { get; set; }
    }

    public class StreetResponse : BaseResponse
    {
        public string StreetName { get; set; }
    }
}
