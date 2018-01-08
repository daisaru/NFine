using System;
using System.Collections.Generic;
using System.Linq;

using NFine.Domain.Entity.Business;
using NFine.Domain.IRepository.Business;
using NFine.Repository.Business;

namespace NFine.Application.Business
{
    public class RoomManageApp
    {
        private IRoomRepository service = new RoomRepository();

        public List<RoomEntity> GetList()
        {
            List<RoomEntity> rooms = service.IQueryable().ToList();
            return rooms;
        }

        public RoomEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }

        public void DeleteForm(string keyValue)
        {
            if(service.IQueryable().Count( t => t.F_ParentRoomId.Equals(keyValue)) > 0)
            {
                throw new Exception("删除失败！操作的对象包含了下级数据。");
            }
            else
            {
                service.Delete(t => t.F_Id == keyValue);
            }
        }


        public void SubmitForm(RoomEntity roomEntity, string roomId)
        {
            if(!string.IsNullOrEmpty(roomId))
            {
                roomEntity.Modify(roomId);
                service.Update(roomEntity);
            }
            else
            {
                roomEntity.Create();
                service.Insert(roomEntity);
            }
        }

    }
}
