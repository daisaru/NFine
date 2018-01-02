using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.Entity.Business
{
    public class RoomEntity : IEntity<RoomEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        public string F_Id { get; set; }

        public string F_CommunityName { get; set; }
        public string F_OwnerId { get; set; }
        public string F_ProvinceId { get; set; }
        public string F_CityId { get; set; }
        public string F_TownId { get; set; }
        public string F_Address { get; set; }
        public string F_Lng { get; set; }
        public string F_Lat { get; set; }
        public string F_AddressNumber { get; set; }
        public string F_BuildingNumber { get; set; }
        public string F_SectionNumber { get; set; }
        public string F_RoomNumber { get; set; }
        public string F_RoomCertificate { get; set; }

        public string F_Description { get; set; }
        public DateTime? F_CreatorTime { get; set; }
        public string F_CreatorUserId { get; set; }
        public DateTime? F_LastModifyTime { get; set; }
        public string F_LastModifyUserId { get; set; }
        public bool? F_DeleteMark { get; set; }
        public DateTime? F_DeleteTime { get; set; }
        public string F_DeleteUserId { get; set; }
    }
}
