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
        private IRoomRepository roomService = new RoomRepository();

        public List<RoomEntity> GetList()
        {
            List<RoomEntity> rooms = roomService.IQueryable().ToList();
            return rooms;
        }
    }
}
