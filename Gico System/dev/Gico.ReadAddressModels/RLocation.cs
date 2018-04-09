namespace Gico.ReadAddressModels
{
    public class RLocation
    {
        public int ProvinceId { get; set; }
        public string ProvincePrefix { get; set; }
        public string ProvinceName { get; set; }
        public string ProvinceShortName { get; set; }
        public int DistrictId { get; set; }
        public string DistrictPrefix { get; set; }
        public string DistrictName { get; set; }
        public int WardId { get; set; }
        public string WardPrefix { get; set; }
        public string WardName { get; set; }
        public int? StreetId { get; set; }
        public string StreetPrefix { get; set; }
        public string StreetName { get; set; }

        public string Key => $"{ProvinceId}_{DistrictId}_{WardId}_{StreetId.GetValueOrDefault()}";

        public string FullAddress
        {
            get
            {
                if (string.IsNullOrEmpty(StreetName))
                {
                    return $"{WardName}, {DistrictName}, {ProvinceName}";
                }
                else
                {
                    return $"{StreetName}, {WardName}, {DistrictName}, {ProvinceName}";
                }

            }
        }
    }
}