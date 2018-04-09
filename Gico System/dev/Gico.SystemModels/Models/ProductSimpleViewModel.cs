using Gico.Config;

namespace Gico.SystemModels.Models
{
    public class ProductSimpleViewModel
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public EnumDefine.ProductStatus Status { get; set; }
        public EnumDefine.ProductType Type { get; set; }

        public string StatusName => Status.ToString();
        public string TypeName => Type.ToString();
    }

    public class ProductGroupProductViewModel : ProductSimpleViewModel
    {
        public bool IsAdd { get; set; }
    }
}