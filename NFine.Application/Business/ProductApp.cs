using System;
using System.Collections.Generic;
using System.Linq;

using NFine.Domain.Entity.Business;
using NFine.Domain.IRepository.Business;
using NFine.Repository.Business;


namespace NFine.Application.Business
{
    public class ProductApp
    {
        private IProductRepository service = new ProductRepository();

        public List<ProductEntity> GetList()
        {
            return service.IQueryable().ToList();
        }
    }
}
