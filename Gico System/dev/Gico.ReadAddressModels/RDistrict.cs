namespace Gico.ReadAddressModels
{
    public class RDistrict
    {
        public int Id { get; set; }
        public int ProvinceId { get; set; }
        public string Prefix { get; set; }
        public string DistrictName { get; set; }
        public string DistrictNameEN { get; set; }
        public string ProvinceName { get; set; }
        public string ShortName { get; set; }
    }
}