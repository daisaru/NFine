using System.Data.Entity.ModelConfiguration;
using NFine.Domain.Entity.Business;

namespace NFine.Mapping.Business
{
    public class DeviceMap : EntityTypeConfiguration<DeviceEntity>
    {
        public DeviceMap()
        {
            this.ToTable("Buz_Device");
            this.HasKey(t => t.F_Id);
        }
    }
}