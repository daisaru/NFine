using System.Data.Entity.ModelConfiguration;
using NFine.Domain.Entity.Business;

namespace NFine.Mapping.Business
{
    public class ProductMap : EntityTypeConfiguration<ProductEntity>
    {
        public ProductMap()
        {
            this.ToTable("Product");
            this.HasKey(t => t.F_Id);
        }
    }
}
