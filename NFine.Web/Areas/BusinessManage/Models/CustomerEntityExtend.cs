using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using NFine.Domain.Entity.Business;

namespace NFine.Web.Areas.BusinessManage.Models
{
    public class CustomerEntityExtend : CustomerEntity
    {
        public string F_CustomerTypeName { get; set; }
        public int F_CustomerTypeLevel { get; set; }
    }
}