using Store.Common.Constants;
using Store.Interfaces.Communication;
using Store.Model.Entities;
using Store.Web.Controllers.Base;
using Store.Web.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Store.Web.Controllers.Web
{
    public class ProductsController : ShopItemsControllerBase<Product>
    {
        protected override string Endpoint => Endpoints.Products;

        public ProductsController(IRestClient restClient)
            : base(restClient)
        {
        }

        [HttpPost]
        public async Task<ActionResult> Index(CreateProductModel viewModel)
        {
            var product = new Product
            {
                Name = viewModel.Product,
                Description = viewModel.Description,
                CategoryId = ParseInt(viewModel.CategoryId)
            };

            await PostEntity(product);

            return await Index();
        }

        public override async Task<ActionResult> Create()
        {
            var categories = await GetEntities<IEnumerable<ProductCategory>>(Endpoints.ProductCategory);

            var createModel = new CreateProductModel
            {
                Categories = GetDropDownList(categories)
            };

            return View(createModel);
        }
    }
}
