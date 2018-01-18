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

        public DeviceEntity GetForm(string keyValue)
        {
            return deviceRepository.FindEntity(keyValue);
        }

        public void DeleteForm(string keyValue)
        {
            if (deviceRepository.IQueryable().Count(t => t.F_ParentId.Equals(keyValue)) > 0)
            {
                throw new Exception("删除失败！操作的对象包含了下级数据。");
            }
            else
            {
                deviceRepository.Delete(t => t.F_Id == keyValue);
            }
        }

        public void SubmitForm(DeviceEntity deviceEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                deviceEntity.Modify(keyValue);
                deviceRepository.Update(deviceEntity);
            }
            else
            {
                deviceEntity.Create();
                deviceRepository.Insert(deviceEntity);
            }
        }

        public List<DeviceEntity> GetList()
        {
            List<DeviceEntity> devices = deviceRepository.IQueryable().OrderBy(t => t.F_Brand).ToList();
            return devices;
        }

        public void UnbindDevice(string deviceId)
        {
            var expBind = ExtLinq.True<RoomDeviceEntity>();
            expBind = expBind.And(t => t.F_DeviceId == deviceId);
            var tmp = roomDeviceRepository.FindEntity(expBind);
            tmp.Remove();
            roomDeviceRepository.Delete(tmp);
        }

        public void SubmitBindForm(DeviceEntity device, string roomId)
        {
            RoomDeviceEntity entity = new RoomDeviceEntity();
            entity.Create();
            entity.F_DeviceId = device.F_Id;
            entity.F_RoomId = roomId;
            roomDeviceRepository.Insert(entity);
        }

        public List<DeviceEntity> GetUnbindDevices(string typeId)
        {
            List<DeviceEntity> devices = new List<DeviceEntity>();
            var expTypeId = ExtLinq.True<DeviceEntity>();

            if(!string.IsNullOrEmpty(typeId))
            {
                expTypeId = expTypeId.And(t => t.F_DeviceTypeId == typeId);
            }

            devices = deviceRepository.IQueryable(expTypeId).ToList();

            List<DeviceEntity> ret = new List<DeviceEntity>();
            foreach(DeviceEntity device in devices)
            {

                var expBind = ExtLinq.True<RoomDeviceEntity>();
                expBind = expBind.And(t => t.F_DeviceId == device.F_Id);
                var tmp = roomDeviceRepository.FindEntity(expBind);
                if(tmp == null)
                {
                    ret.Add(device);
                }
            }

            return ret;
        }

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

            if(deviceIds.Count > 0)
            {
                ret = deviceRepository.IQueryable(expDevice).ToList();
            }

            return ret;
        }
    }
}
