using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using NFine.Web;
using NFine.Code;
using NFine.Application.Business;
using NFine.Domain.Entity.Business;

namespace NFine.Web.Areas.BusinessManage.Controllers
{
    public class RoomController : ControllerBase
    {
        private RoomManageApp roomManageApp = new RoomManageApp();

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
                treeModel.entityJson = item.ToJson();
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
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            roomManageApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }

    }
}