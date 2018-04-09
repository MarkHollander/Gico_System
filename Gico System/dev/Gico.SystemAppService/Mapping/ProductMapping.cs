using Gico.ReadSystemModels.Product;
using Gico.SystemModels.Models;

namespace Gico.SystemAppService.Mapping
{
    public static class ProductMapping
    {
        public static ProductViewModel ToModel(this RProduct product)
        {
            if (product == null)
            {
                return null;
            }
            return new ProductViewModel()
            {
                
            };

        }
        public static ProductSimpleViewModel ToSimpleModel(this RProduct product)
        {
            if (product == null)
            {
                return null;
            }
            return new ProductSimpleViewModel()
            {
                Status = product.Status,
                Id = product.Id,
                Name = product.Name,
                Type = product.Type,
                Code = product.Code,
                
            };

        }
        
    }
}