using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using NFine.Web;
using NFine.Code;
using NFine.Application.Business;
using NFine.Application.SystemManage;
using NFine.Domain.Entity.Business;
using NFine.Domain.Entity.SystemManage;
using NFine.Web.Areas.BusinessManage.Models;

namespace NFine.Web.Areas.BusinessManage.Controllers
{
    public class RoomController : ControllerBase
    {
        private RoomManageApp roomManageApp = new RoomManageApp();
        private AreaApp areaApp = new AreaApp();
        private CustomerApp customerApp = new CustomerApp();
        private DeviceApp deviceApp = new DeviceApp();
        private SystemServiceApp systemServiceApp = new SystemServiceApp();

        private List<AreaEntity> areaData = new List<AreaEntity>();
        private List<CustomerEntity> ownerData = new List<CustomerEntity>();
        private List<ItemsDetailEntity> deviceTypeData = new List<ItemsDetailEntity>();
        private List<ItemsDetailEntity> deviceStatusData = new List<ItemsDetailEntity>();

        private const string strOwnerTypeId = "10FB9113-4CC3-40C5-A442-6AFD004E9788";

        public RoomController()
        {
            areaData = areaApp.GetList();
            ownerData = customerApp.GetTypeCustomers(strOwnerTypeId);

            deviceTypeData = systemServiceApp.GetItemsDetail("DeviceType");
            deviceStatusData = systemServiceApp.GetItemsDetail("DeviceStatus");
        }

        [HttpGet]
        [HandlerAuthorize]
        public ActionResult BindDevice()
        {
            return View();
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetDeviceTreeGridJson(string keyValue)
        {
            var devices = deviceApp.GetBindDevices(keyValue);
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

            return Content(treeList.TreeGridJson());
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetDeviceTypeTreeSelectJson()
        {
            var deviceTypes = deviceTypeData;
            
            var treeList = new List<TreeSelectModel>();
            foreach(ItemsDetailEntity item in deviceTypes)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.F_Id;
                treeModel.text = item.F_ItemName;
                treeModel.parentId = item.F_ParentId == null ? "0" : item.F_ParentId;
                treeModel.data = deviceApp.GetUnbindDevices(item.F_Id);
                treeList.Add(treeModel);
            }
            string tmp = treeList.TreeSelectJson();
            return Content(tmp);
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetAddressTreeSelectJson()
        {
            var data = areaApp.GetList();
            var treeList = new List<TreeSelectModel>();
            foreach (AreaEntity item in data)
            {
                if(item.F_ParentId == "0")
                {
                    TreeSelectModel treeModel = new TreeSelectModel();
                    treeModel.id = item.F_Id;
                    treeModel.text = item.F_FullName;
                    treeModel.parentId = item.F_ParentId;
                    treeModel.data = data.Where(t => t.F_ParentId == item.F_Id).ToList();
                    treeList.Add(treeModel);
                }
            }
            string tmp = treeList.TreeSelectJson();
            return Content(tmp);
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetOwnerSelectJson()
        {
            var treeList = new List<TreeSelectModel>();
            foreach (CustomerEntity item in ownerData)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.F_Id;
                treeModel.text = item.F_RealName;
                treeModel.parentId = "0";
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeSelectJson());
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetTreeSelectJson()
        {
            var data = roomManageApp.GetList();
            var treeList = new List<TreeSelectModel>();
            foreach(RoomEntity item in data)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.F_Id;
                treeModel.text = item.F_RoomName;
                treeModel.parentId = item.F_ParentRoomId;
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeSelectJson());
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetRoomsTreeGridJson(string keyword)
        {
            var data = roomManageApp.GetList();
            var treeList = new List<TreeGridModel>();

            foreach(RoomEntity item in data)
            {
                TreeGridModel treeModel = new TreeGridModel();
                bool hasChildren = data.Count(t => t.F_ParentRoomId == item.F_Id) == 0 ? false : true;
                treeModel.id = item.F_Id;
                treeModel.text = item.F_RoomName;
                treeModel.isLeaf = hasChildren;
                treeModel.parentId = item.F_ParentRoomId;
                treeModel.expanded = true;

                RoomEntityExtend roomEntityExtend = new RoomEntityExtend(item);
                roomEntityExtend.F_ProvinceName = areaData.Where(t => t.F_Id == item.F_ProvinceId).ToList()[0].F_FullName.Trim();
                roomEntityExtend.F_CityName = areaData.Where(t => t.F_Id == item.F_CityId).ToList()[0].F_FullName.Trim();
                roomEntityExtend.F_OwnerName = ownerData.Where(t => t.F_Id == item.F_OwnerId).ToList()[0].F_RealName.Trim();

                treeModel.entityJson = roomEntityExtend.ToJson();
                treeList.Add(treeModel);
            }

            if(!string.IsNullOrEmpty(keyword))
            {
                treeList = treeList.TreeWhere(t => t.text.Contains(keyword), "id", "parentId");
            }

            return Content(treeList.TreeGridJson());
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFromJson(string keyValue)
        {
            var data = roomManageApp.GetForm(keyValue);
            return Content(data.ToJson());
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(RoomEntity roomEntity, string keyValue)
        {
            roomManageApp.SubmitForm(roomEntity, keyValue);
            return Success("操作成功。");
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitFormBindDevice(DeviceEntity deviceEntity, string keyValue)
        {
            deviceApp.SubmitBindForm(deviceEntity, keyValue);
            return Success("操作成功。");
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            roomManageApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }

    }
}