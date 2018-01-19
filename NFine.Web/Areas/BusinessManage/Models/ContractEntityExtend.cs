using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using NFine.Domain.Entity.Business;
using NFine.Web.Utils;

namespace NFine.Web.Areas.BusinessManage.Models
{
    public class ContractEntityExtend : ContractEntity
    {
        public string F_RoomName { get; set; }
        public string F_SectionName { get; set; }
        public string F_CustomerName { get; set; }
        public string F_ContractStatusName { get; set; }

        public ContractEntityExtend(ContractEntity item)
        {
            Tools.CopyModel(this, item);
        }
    }
}