using Store.Interfaces.Repository;
using Store.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.DataAccessLayer.Repository
{
    public class ProductCategoryRepository : RepositoryBase<ProductCategory>
    {
        public ProductCategoryRepository(IStoreContext context)
            : base(context)
        {
        }
    }
}
