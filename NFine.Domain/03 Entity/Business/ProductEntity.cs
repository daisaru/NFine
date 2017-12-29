using System;

namespace NFine.Domain.Entity.Business
{
    public class ProductEntity : IEntity<ProductEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        public string F_Id { get; set; }

        public string F_Name { get; set; }
        public string F_Description { get; set; }
        public string F_Category { get; set; }
        public decimal F_Price { get; set; }

        public bool? F_DeleteMark { get; set; }
        public DateTime? F_CreatorTime { get; set; }
        public string F_CreatorUserId { get; set; }
        public DateTime? F_LastModifyTime { get; set; }
        public string F_LastModifyUserId { get; set; }
        public DateTime? F_DeleteTime { get; set; }
        public string F_DeleteUserId { get; set; }
    }
}
