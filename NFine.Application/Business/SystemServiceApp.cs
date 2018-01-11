using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NFine.Code;
using NFine.Domain.Entity.SystemManage;
using NFine.Domain.IRepository.SystemManage;
using NFine.Repository.SystemManage;


namespace NFine.Application.Business
{
    public class SystemServiceApp
    {
        private IItemsRepository serviceItem = new ItemsRepository();
        private IItemsDetailRepository serviceItemDetails = new ItemsDetailRepository();

        public List<ItemsDetailEntity> GetItemsDetail(string encode)
        {
            var expression = ExtLinq.True<ItemsEntity>();
            if (!string.IsNullOrEmpty(encode))
            {
                expression = expression.And(t => t.F_EnCode.Contains(encode));
            }
            ItemsEntity itemType = serviceItem.FindEntity(expression);

            List<ItemsDetailEntity> details = new List<ItemsDetailEntity>();
            details = serviceItemDetails.IQueryable().ToList().Where(t => t.F_ItemId == itemType.F_Id).ToList();

            return details;
        }
    }
}
