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
    public class CustomerController : ControllerBase
    {
        private CustomerApp customerApp = new CustomerApp();
        private SystemServiceApp systemServiceApp = new SystemServiceApp();

        private List<ItemsDetailEntity> customerTypeData = new List<ItemsDetailEntity>();
        private List<ItemsDetailEntity> customerLevelData = new List<ItemsDetailEntity>();

        public CustomerController()
        {
            customerTypeData = systemServiceApp.GetItemsDetail("CustomerType");
            customerLevelData = systemServiceApp.GetItemsDetail("CustomerLevel");
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetCustomerTreeGridJson(string keyword)
        {
            var data = customerApp.GetCustomers();
            var treeList = new List<TreeGridModel>();

            foreach (CustomerEntity item in data)
            {
                TreeGridModel treeModel = new TreeGridModel();
                bool hasChildren = data.Count(t => t.F_ParentId == item.F_Id) == 0 ? false : true;
                treeModel.id = item.F_Id;
                treeModel.text = item.F_RealName;
                treeModel.isLeaf = hasChildren;
                treeModel.parentId = item.F_ParentId;
                treeModel.expanded = true;

                CustomerEntityExtend customerEntityExtend = new CustomerEntityExtend(item);
                customerEntityExtend.F_CustomerTypeName = customerTypeData.Where(t => t.F_Id == item.F_CustomerTypeId).ToList()[0].F_ItemName.Trim();
                customerEntityExtend.F_CustomerLevel = customerLevelData.Where(t => t.F_Id == item.F_CustomerLevelId).ToList()[0].F_ItemName.Trim();
                

                treeModel.entityJson = customerEntityExtend.ToJson();
                treeList.Add(treeModel);
            }

            if (!string.IsNullOrEmpty(keyword))
            {
                treeList = treeList.TreeWhere(t => t.text.Contains(keyword), "id", "parentId");
            }

            return Content(treeList.TreeGridJson());
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            customerApp.DeleteCustomerForm(keyValue);
            return Success("删除成功。");
        }

        #region 客户表单

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = customerApp.GetCustomerForm(keyValue);
            return Content(data.ToJson());
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetTreeSelectJson()
        {
            var data = customerApp.GetCustomers();
            var treeList = new List<TreeSelectModel>();
            foreach (CustomerEntity item in data)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.F_Id;
                treeModel.text = item.F_RealName;
                treeModel.parentId = item.F_ParentId;
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeSelectJson());
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetCustomerTypeSelectJson()
        {
            var treeList = new List<TreeSelectModel>();
            foreach (ItemsDetailEntity item in customerTypeData)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.F_Id;
                treeModel.text = item.F_ItemName;
                treeModel.parentId = "0";
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeSelectJson());
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetCustomerLevelSelectJson()
        {
            var treeList = new List<TreeSelectModel>();
            foreach (ItemsDetailEntity item in customerLevelData)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.F_Id;
                treeModel.text = item.F_ItemName;
                treeModel.parentId = "0";
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeSelectJson());
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(CustomerEntity customerEntity, string keyValue)
        {
            customerApp.SubmitCustomerForm(customerEntity, keyValue);
            return Success("操作成功。");
        }

        #endregion

        #region 账户表单

        [HttpGet]
        [HandlerAuthorize]
        public ActionResult Account()
        {
            return View();
        }

        [HttpGet]
        [HandlerAuthorize]
        public ActionResult AccountDetails()
        {
            return View();
        }

        [HttpGet]
        [HandlerAuthorize]
        public ActionResult AccountForm()
        {
            return View();
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitAccountForm(CustomerAccountEntity customerAccountEntity, string keyValue, string F_CustomerId)
        {
            customerApp.SubmitCustomerAccountForm(customerAccountEntity, keyValue);
            return Success("操作成功。");
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAccountForm(string keyValue)
        {
            customerApp.DeleteCustomerAccountForm(keyValue);
            return Success("删除成功。");
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetAccountTreeGridJson(string keyword, string customerId)
        {
            var data = customerApp.GetCustomerAccounts(customerId);
            var treeList = new List<TreeGridModel>();

            foreach (CustomerAccountEntity item in data)
            {
                TreeGridModel treeModel = new TreeGridModel();
                bool hasChildren = data.Count(t => t.F_ParentId == item.F_Id) == 0 ? false : true;
                treeModel.id = item.F_Id;
                treeModel.text = item.F_AccountName;
                treeModel.isLeaf = hasChildren;
                treeModel.parentId = item.F_ParentId;
                treeModel.expanded = true;             

                treeModel.entityJson = item.ToJson();
                treeList.Add(treeModel);
            }

            if (!string.IsNullOrEmpty(keyword))
            {
                treeList = treeList.TreeWhere(t => t.text.Contains(keyword), "id", "parentId");
            }

            return Content(treeList.TreeGridJson());
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetAccountDetailFormJson(string keyValue)
        {
            var data = customerApp.GetCustomerAccountForm(keyValue);
            return Content(data.ToJson());
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetAccountTreeSelectJson(string customerId)
        {
            var data = customerApp.GetCustomerAccounts(customerId);
            var treeList = new List<TreeSelectModel>();
            foreach (CustomerAccountEntity item in data)
            {
                if (item.F_ParentId == "0")
                {
                    TreeSelectModel treeModel = new TreeSelectModel();
                    treeModel.id = item.F_Id;
                    treeModel.text = item.F_AccountName;
                    treeModel.parentId = item.F_ParentId;
                    treeModel.data = data.Where(t => t.F_ParentId == item.F_Id).ToList();
                    treeList.Add(treeModel);
                }
            }
            string tmp = treeList.TreeSelectJson();
            return Content(tmp);
        }


        #endregion
    }
}