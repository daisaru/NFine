using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using NFine.Domain.Entity.Business;
using NFine.Web.Utils;

namespace NFine.Web.Areas.BusinessManage.Models
{
    public class DeviceEntityExtend : DeviceEntity
    {
        public string F_DeviceTypeName { get; set; }
        public string F_StatusName { get; set; }
        public DeviceEntityExtend(DeviceEntity device)
        {
            Tools.CopyModel(this, device);
        }
    }
}