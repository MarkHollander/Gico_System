namespace Gico.FrontEndModels.Models
{
    public class LocationViewModel
    {
        public int ProvinceId { get; set; }
        public int DistrictId { get; set; }
        public int WardId { get; set; }
        public int? StreetId { get; set; }
        public string FullAddress { get; set; }

        public string Id => $"{ProvinceId}_{DistrictId}_{WardId}_{StreetId.GetValueOrDefault()}";
    }
}