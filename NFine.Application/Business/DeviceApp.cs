using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NFine.Code;
using NFine.Domain.Entity.Business;
using NFine.Domain.IRepository.Business;
using NFine.Repository.Business;

namespace NFine.Application.Business
{
    public class DeviceApp
    {
        private IDeviceRepository deviceRepository = new DeviceRepository();
        private IRoomDeviceRepository roomDeviceRepository = new RoomDeviceRepository();

        public List<DeviceEntity> GetBindDevices(string keyValue)
        {
            List<DeviceEntity> ret = new List<DeviceEntity>();

            var expDeviceId = ExtLinq.True<RoomDeviceEntity>();
            if (!string.IsNullOrEmpty(keyValue))
            {
                expDeviceId = expDeviceId.And(t => t.F_RoomId == keyValue);
            }
            List<RoomDeviceEntity> deviceIds = roomDeviceRepository.IQueryable(expDeviceId).ToList();

            var expDevice = ExtLinq.True<DeviceEntity>();
            bool bFirst = true;
            foreach(RoomDeviceEntity id in deviceIds)
            {
                if(bFirst)
                {
                    expDevice = expDevice.And(t => t.F_Id == id.F_DeviceId);
                    bFirst = false;
                }
                else
                {
                    expDevice = expDevice.Or(t => t.F_Id == id.F_DeviceId);
                }             
            }

            ret = deviceRepository.IQueryable(expDevice).ToList();

            return ret;
        }
    }
}
