namespace Gico.FrontEndModels.Models
{
    public class AddressViewModel
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Mobile { get; set; }
        public string CountryId { get; set; }
        public string ProvinceId { get; set; }
        public string DistrictId { get; set; }
        public string WardId { get; set; }
        public string StreetId { get; set; }
        public string CountryName { get; set; }
        public string ProvinceName { get; set; }
        public string DistrictName { get; set; }
        public string WardName { get; set; }
        public string StreetName { get; set; }
        public string Detail { get; set; }
        public string Description { get; set; }

        public string AddressFull => $"{Detail}-{StreetName}-{WardName}-{ProvinceName}";
    }
}