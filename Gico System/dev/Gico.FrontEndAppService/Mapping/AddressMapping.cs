using Gico.FrontEndModels.Models;
using Gico.ReadOrderModels;

namespace Gico.FrontEndAppService.Mapping
{
    public static class AddressMapping
    {
        public static AddressViewModel ToModel(this RAddress address)
        {
            if (address == null)
            {
                return null;
            }
            return new AddressViewModel()
            {
                Id = address.Id,
                FullName = address.FullName,
                CountryId = address.CountryId,
                CountryName = address.CountryName,
                Description = address.Description,
                Detail = address.Detail,
                DistrictId = address.DistrictId,
                DistrictName = address.DistrictName,
                Mobile = address.Mobile,
                ProvinceId = address.ProvinceId,
                ProvinceName = address.ProvinceName,
                StreetId = address.StreetId,
                StreetName = address.StreetName,
                WardId = address.WardId,
                WardName = address.WardName
            };
        }
    }
}