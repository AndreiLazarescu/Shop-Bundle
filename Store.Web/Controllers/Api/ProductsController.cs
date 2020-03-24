using Store.Interfaces.Repository;
using Store.Model.Entities;
using Store.Web.Controllers.Base;

namespace Store.Web.Controllers.Api
{
    public class ProductsController : ApiControllerBase<Product>
    {
        public ProductsController(IRepository<Product> repository)
            : base(repository) { }
    }
}
