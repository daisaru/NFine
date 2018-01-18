using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using NFine.Domain.Entity.Business;
using NFine.Web.Utils;

namespace NFine.Web.Areas.BusinessManage.Models
{
    public class RoomEntityExtend : RoomEntity
    {
        public string F_ProvinceName { get; set; }
        public string F_CityName { get; set; }
        public string F_TownName { get; set; }
        public string F_OwnerName { get; set; }

        public string F_ContractStatus { get; set; }

        public RoomEntityExtend(RoomEntity room)
        {
            Tools.CopyModel(this, room);          
        }
    }
}