using System.Data.Entity.ModelConfiguration;
using NFine.Domain.Entity.Business;

namespace NFine.Mapping.Business
{
    public class DeviceCustomerMap : EntityTypeConfiguration<DeviceCustomerEntity>
    {
        public DeviceCustomerMap()
        {
            this.ToTable("Buz_DeviceCustomer");
            this.HasKey(t => t.F_Id);
        }
    }
}