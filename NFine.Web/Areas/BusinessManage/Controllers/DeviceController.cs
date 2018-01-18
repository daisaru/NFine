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
    public class DeviceController : ControllerBase
    {
        private DeviceApp deviceApp = new DeviceApp();
        private SystemServiceApp systemServiceApp = new SystemServiceApp();

        private List<ItemsDetailEntity> deviceTypeData = new List<ItemsDetailEntity>();
        private List<ItemsDetailEntity> deviceStatusData = new List<ItemsDetailEntity>();

        public DeviceController()
        {
            deviceTypeData = systemServiceApp.GetItemsDetail("DeviceType");
            deviceStatusData = systemServiceApp.GetItemsDetail("DeviceStatus");
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetDeviceTreeGridJson(string keyword)
        {
            var devices = deviceApp.GetList();
            var treeList = new List<TreeGridModel>();

            foreach (DeviceEntity item in devices)
            {
                TreeGridModel treeModel = new TreeGridModel();
                bool hasChildren = devices.Count(t => t.F_ParentId == item.F_Id) == 0 ? false : true;
                treeModel.id = item.F_Id;
                treeModel.text = item.F_DeviceName;
                treeModel.isLeaf = hasChildren;
                treeModel.parentId = item.F_ParentId;
                treeModel.expanded = true;

                DeviceEntityExtend deviceEntityExtend = new DeviceEntityExtend(item);
                deviceEntityExtend.F_DeviceTypeName = deviceTypeData.Find(t => t.F_Id == item.F_DeviceTypeId).F_ItemName;
                deviceEntityExtend.F_StatusName = deviceStatusData.Find(t => t.F_Id == item.F_StatusId).F_ItemName;

                treeModel.entityJson = deviceEntityExtend.ToJson();
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
            deviceApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }

        #region 设备表单

        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(DeviceEntity deviceEntity, string keyValue)
        {
            deviceApp.SubmitForm(deviceEntity, keyValue);
            return Success("操作成功。");
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = deviceApp.GetForm(keyValue);
            return Content(data.ToJson());
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetTreeSelectJson()
        {
            var data = deviceApp.GetList();
            var treeList = new List<TreeSelectModel>();
            foreach (DeviceEntity item in data)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.F_Id;
                treeModel.text = item.F_DeviceName;
                treeModel.parentId = item.F_ParentId;
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeSelectJson());
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetDeviceTypeSelectJson()
        {
            var treeList = new List<TreeSelectModel>();
            foreach (ItemsDetailEntity item in deviceTypeData)
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
        public ActionResult GetStatusSelectJson()
        {
            var treeList = new List<TreeSelectModel>();
            foreach (ItemsDetailEntity item in deviceStatusData)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.F_Id;
                treeModel.text = item.F_ItemName;
                treeModel.parentId = "0";
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeSelectJson());
        }

        #endregion
    }
}