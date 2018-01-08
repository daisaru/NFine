using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.Entity.Business
{
    public class DeviceStatusEntity : IEntity<DeviceStatusEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        public string F_Id { get; set; }

        public string F_DeviceStatusName { get; set; }

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
