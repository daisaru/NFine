using System.Data.Entity.ModelConfiguration;
using NFine.Domain.Entity.Business;

namespace NFine.Mapping.Business
{
    public class CustomerTypeMap : EntityTypeConfiguration<CustomerTypeEntity>
    {
        public CustomerTypeMap()
        {
            this.ToTable("Buz_CustomerType");
            this.HasKey(t => t.F_Id);
        }
    }
}