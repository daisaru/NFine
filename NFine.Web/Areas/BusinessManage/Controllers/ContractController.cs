using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using NFine.Code;
using NFine.Application.Business;
using NFine.Application.SystemManage;
using NFine.Domain.Entity.Business;
using NFine.Domain.Entity.SystemManage;
using NFine.Web.Areas.BusinessManage.Models;

namespace NFine.Web.Areas.BusinessManage.Controllers
{
    public class ContractController : ControllerBase
    {
        private ContractApp contractApp = new ContractApp();
        private SystemServiceApp systemServiceApp = new SystemServiceApp();
        private CustomerApp customerApp = new CustomerApp();
        private RoomManageApp roomApp = new RoomManageApp();

        private List<ItemsDetailEntity> customerTypeData = new List<ItemsDetailEntity>();
        private List<ItemsDetailEntity> customerLevelData = new List<ItemsDetailEntity>();
        private List<ItemsDetailEntity> contractStatusData = new List<ItemsDetailEntity>();

        public ContractController()
        {
            contractStatusData = systemServiceApp.GetItemsDetail("ContractStatus");
            customerTypeData = systemServiceApp.GetItemsDetail("CustomerType");
            customerLevelData = systemServiceApp.GetItemsDetail("CustomerLevel");
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetContractTreeGridJson(string keyword)
        {
            var data = contractApp.GetContracts();
            var treeList = new List<TreeGridModel>();

            foreach (ContractEntity item in data)
            {
                TreeGridModel treeModel = new TreeGridModel();
                bool hasChildren = data.Count(t => t.F_ParentId == item.F_Id) == 0 ? false : true;
                treeModel.id = item.F_Id;
                treeModel.text = item.F_ContractName;
                treeModel.isLeaf = hasChildren;
                treeModel.parentId = item.F_ParentId;
                treeModel.expanded = true;

                ContractEntityExtend contractEntityExtend = new ContractEntityExtend(item);
                contractEntityExtend.F_RoomName = roomApp.GetForm(item.F_RoomId).F_RoomName;
                contractEntityExtend.F_SectionName = roomApp.GetForm(item.F_SectionId).F_RoomName;
                contractEntityExtend.F_CustomerName = customerApp.GetCustomerForm(item.F_CustomerId).F_RealName;
                contractEntityExtend.F_ContractStatusName = contractStatusData.Where(t => t.F_Id == item.F_ContractStatusId).ToList()[0].F_ItemName;

                treeModel.entityJson = contractEntityExtend.ToJson();
                treeList.Add(treeModel);
            }

            if (!string.IsNullOrEmpty(keyword))
            {
                treeList = treeList.TreeWhere(t => t.text.Contains(keyword), "id", "parentId");
            }

            return Content(treeList.TreeGridJson());
        }

        #region 合约表单

        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            contractApp.DeleteContractForm(keyValue);
            return Success("删除成功。");
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(ContractEntity contractEntity, string keyValue)
        {
            contractApp.SubmitContractForm(contractEntity, keyValue);
            return Success("操作成功。");
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = contractApp.GetContractForm(keyValue);
            return Content(data.ToJson());
        }

        #endregion
    }
}