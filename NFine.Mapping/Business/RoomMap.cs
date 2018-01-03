using System.Data.Entity.ModelConfiguration;
using NFine.Domain.Entity.Business;

namespace NFine.Mapping.Business
{
    public class RoomMap : EntityTypeConfiguration<RoomEntity>
    {
        public RoomMap()
        {
            this.ToTable("Buz_Room");
            this.HasKey(t => t.F_Id);
        }
    }
}