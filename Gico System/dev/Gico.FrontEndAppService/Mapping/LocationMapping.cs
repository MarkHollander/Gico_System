using Gico.FrontEndModels.Models;
using Gico.ReadAddressModels;

namespace Gico.FrontEndAppService.Mapping
{
    public static class LocationMapping
    {
        public static LocationViewModel ToModel(this RLocation location)
        {
            if (location == null) return null;
            return new LocationViewModel()
            {
                DistrictId = location.DistrictId,
                WardId = location.WardId,
                ProvinceId = location.ProvinceId,
                StreetId = location.StreetId,
                FullAddress = location.FullAddress
            };
        }
    }
}