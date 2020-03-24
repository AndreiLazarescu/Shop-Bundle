using Store.Model.Entities;
using System.Linq.Expressions;

namespace Store.DataAccessLayer.Repository
{
    public class ProductRepository : RepositoryBase<Product>
    {
        public ProductRepository(IStoreContext context) : base(context)
        {
            BaseIncludes = new Expression<System.Func<Product, object>>[]
            {
                x => x.Category
            };
        }

        protected override Product ProcessEntity(Product entity)
        {
            if (entity.CategoryId > 0)
            {
                entity.Category = null;
            }

            return base.ProcessEntity(entity);
        }
    }
}
