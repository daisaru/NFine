using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.Entity.Business
{
    public class CustomerAccountEntity : IEntity<CustomerAccountEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        public string F_Id { get; set; }

        public string F_ParentId { get; set; }
        public string F_CustomerId { get; set; }
        public string F_AccountName { get; set; }
        public string F_AccountId { get; set; }
        public bool F_DefaultAccount { get; set; }
        public string F_BankName { get; set; }
        public string F_BankCode { get; set; }
        public DateTime F_ValidStart { get; set; }
        public DateTime F_ValidEnd { get; set; }

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
