namespace Gico.ReadAddressModels
{
    public class RWard
    {
        public int Id { get; set; }
        public string Prefix { get; set; }
        public string WardName { get; set; }
        public string WardNameEN { get; set; }
        public string ShortName { get; set; }
        public int DistrictId { get; set; }
        public string DistrictName { get; set; }
        public int ProvinceId { get; set; }
        public string ProvinceName { get; set; }
    }
}