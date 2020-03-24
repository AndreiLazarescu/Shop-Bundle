using Store.Interfaces.Repository;
using Store.Model.Entities;
using Store.Web.Controllers.Base;

namespace Store.Web.Controllers.Api
{
    public class ProductCategoryController : ApiControllerBase<ProductCategory>
    {
        public ProductCategoryController(IRepository<ProductCategory> repository)
            : base(repository) { }
    }
}
