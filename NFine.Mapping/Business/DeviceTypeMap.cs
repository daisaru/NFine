using System.Data.Entity.ModelConfiguration;
using NFine.Domain.Entity.Business;

namespace NFine.Mapping.Business
{
    public class DeviceTypeMap : EntityTypeConfiguration<DeviceTypeEntity>
    {
        public DeviceTypeMap()
        {
            this.ToTable("Buz_DeviceType");
            this.HasKey(t => t.F_Id);
        }
    }
}