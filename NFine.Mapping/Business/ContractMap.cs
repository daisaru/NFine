using System.Data.Entity.ModelConfiguration;
using NFine.Domain.Entity.Business;

namespace NFine.Mapping.Business
{
    public class ContractMap : EntityTypeConfiguration<ContractEntity>
    {
        public ContractMap()
        {
            this.ToTable("Buz_Contract");
            this.HasKey(t => t.F_Id);
        }
    }
}
