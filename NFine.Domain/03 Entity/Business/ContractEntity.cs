using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.Entity.Business
{
    public class ContractEntity : IEntity<ContractEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        public string F_Id { get; set; }

        public string F_ParentId { get; set; }
        public string F_ContractName { get; set; }
        public string F_RoomId { get; set; }
        public string F_SectionId { get; set; }
        public string F_CustomerId { get; set; }
        public decimal F_Price { get; set; }
        public string F_ContractStatusId { get; set; }
        public DateTime F_ContractStart { get; set; }
        public DateTime F_ContractEnd { get; set; }

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
