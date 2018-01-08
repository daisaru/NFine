using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.Entity.Business
{
    public class DeviceEntity : IEntity<DeviceEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        public string F_Id { get; set; }

        public string F_DeviceName { get; set; }
        public string F_ParentDeviceId { get; set; }
        public string F_DeviceTypeId { get; set; }
        public string F_Brand { get; set; }
        public string F_Model { get; set; }
        public string F_HardwareVersion { get; set; }
        public string F_SoftwareVersion { get; set; }
        public string F_Producer { get; set; }
        public DateTime F_ProductTime { get; set; }
        public DateTime F_DeployTime { get; set; }
        public string F_StatusId { get; set; }
        public string F_Description { get; set; }

        public DateTime? F_CreatorTime { get; set; }
        public string F_CreatorUserId { get; set; }
        public DateTime? F_LastModifyTime { get; set; }
        public string F_LastModifyUserId { get; set; }
        public bool? F_DeleteMark { get; set; }
        public DateTime? F_DeleteTime { get; set; }
        public string F_DeleteUserId { get; set; }
    }
}
