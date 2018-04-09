using Gico.Domains;
using Gico.ReadSystemModels.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.SystemDomains.Product
{
    public class Product_Category_Mapping : BaseDomain
    {
        public Product_Category_Mapping()
        {            
        }

        public Product_Category_Mapping(RProduct_Category_Mapping productCategoryMapping)
        {
            ProductId = productCategoryMapping.ProductId;
            CategoryId = productCategoryMapping.CategoryId;
            IsMainCategory = productCategoryMapping.IsMainCategory;
            DisplayOrder = productCategoryMapping.DisplayOrder;
        }

        #region Instance Properties
        public string ProductId { get; private set; }
        public string CategoryId { get; private set; }
        public bool IsMainCategory { get; private set; }
        public int DisplayOrder { get; private set; }
        #endregion Instance Properties
    }
}
