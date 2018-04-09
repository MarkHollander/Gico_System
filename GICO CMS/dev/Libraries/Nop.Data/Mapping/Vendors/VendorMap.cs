using Nop.Core.Domain.Vendors;

namespace Nop.Data.Mapping.Vendors
{
    /// <summary>
    /// Mapping class
    /// </summary>
    public partial class VendorMap : NopEntityTypeConfiguration<Vendor>
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public VendorMap()
        {
            this.ToTable("Vendor");
            this.HasKey(v => v.VendorId);
            this.Ignore(v => v.Id);
            this.Property(v => v.VendorName).IsRequired().HasMaxLength(400);
            this.Property(v => v.Email).HasMaxLength(400);
        }
    }
}