using System.Data.Entity.ModelConfiguration;
using NFine.Domain.Entity.Business;

namespace NFine.Mapping.Business
{
    public class SectionMap : EntityTypeConfiguration<SectionEntity>
    {
        public SectionMap()
        {
            this.ToTable("Buz_Section");
            this.HasKey(t => t.F_Id);
        }
    }
}