using Nop.Core.Domain.Catalog;

namespace Nop.Data.Mapping.Catalog
{
    /// <summary>
    /// Mapping class
    /// </summary>
    public partial class CategoryMap : NopEntityTypeConfiguration<Category>
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public CategoryMap()
        {
            this.ToTable("Category");
            this.Ignore(c => c.Id);
            this.Ignore(c => c.Discount_Id);
            this.HasKey(c => c.CategoryId);            
        }
    }
}