using System.Data.Entity.ModelConfiguration;
using NFine.Domain.Entity.Business;

namespace NFine.Mapping.Business
{
    public class RoomDeviceMap : EntityTypeConfiguration<RoomDeviceEntity>
    {
        public RoomDeviceMap()
        {
            this.ToTable("Buz_RoomDevice");
            this.HasKey(t => t.F_Id);
        }
    }
}