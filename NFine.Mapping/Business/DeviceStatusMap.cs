using System.Data.Entity.ModelConfiguration;
using NFine.Domain.Entity.Business;

namespace NFine.Mapping.Business
{
    public class DeviceStatusMap : EntityTypeConfiguration<DeviceStatusEntity>
    {
        public DeviceStatusMap()
        {
            this.ToTable("Buz_DeviceStatus");
            this.HasKey(t => t.F_Id);
        }
    }
}