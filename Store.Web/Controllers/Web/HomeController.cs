using Store.Common.Constants;
using Store.Interfaces.Communication;
using Store.Model.Entities;
using Store.Web.Controllers.Base;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Store.Web.Controllers.Web
{
    public class HomeController : ShopItemsControllerBase<Product>
    {
        protected override string Endpoint => Endpoints.Products;

        public HomeController(IRestClient restClient)
            : base(restClient) { }

        public override async Task<ActionResult> Index()
        {
            var entities = await GetEntities<IEnumerable<Product>>(Endpoint);

            return View(entities.OrderByDescending(x => x.Id));
        }

    }
}
