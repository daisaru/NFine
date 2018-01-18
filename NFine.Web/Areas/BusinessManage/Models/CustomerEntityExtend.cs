using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using NFine.Domain.Entity.Business;
using NFine.Web.Utils;

namespace NFine.Web.Areas.BusinessManage.Models
{
    public class CustomerEntityExtend : CustomerEntity
    {
        public string F_CustomerTypeName { get; set; }
        public string F_CustomerLevel { get; set; }

        public CustomerEntityExtend(CustomerEntity item)
        {
            Tools.CopyModel(this, item);
        }
    }
}