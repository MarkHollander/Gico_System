using Nop.Core.Domain.Customers;

namespace Nop.Data.Mapping.Customers
{
    /// <summary>
    /// Mapping class
    /// </summary>
    public partial class CustomerMap : NopEntityTypeConfiguration<Customer>
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public CustomerMap()
        {
            this.ToTable("Customer");
            this.HasKey(c => c.CustomerId);
            this.Ignore(u => u.Id);
            this.Property(u => u.Email).HasMaxLength(1000);
            this.Property(u => u.EmailToRevalidate).HasMaxLength(1000);
            
            this.HasMany(c => c.CustomerRoles)
                .WithMany()
                .Map(m => m.ToTable("Customer_CustomerRole_Mapping"));

            this.HasMany(c => c.Addresses)
                .WithMany()
                .Map(m => m.ToTable("CustomerAddresses"));
           
        }
    }
}