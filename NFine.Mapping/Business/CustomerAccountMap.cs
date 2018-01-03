using System.Data.Entity.ModelConfiguration;
using NFine.Domain.Entity.Business;

namespace NFine.Mapping.Business
{
    public class CustomerAccountMap : EntityTypeConfiguration<CustomerAccountEntity>
    {
        public CustomerAccountMap()
        {
            this.ToTable("Buz_CustomerAccount");
            this.HasKey(t => t.F_Id);
        }
    }
}