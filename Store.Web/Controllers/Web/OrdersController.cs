using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using Store.Common.Constants;
using Store.Interfaces.Communication;
using Store.Model.Entities;
using Store.Web.Controllers.Base;
using Store.Web.Model;

namespace Store.Web.Controllers
{
    /// <summary>
    /// The orders view controller.
    /// </summary>
    public class OrdersController : ShopItemsControllerBase<Order>
    {
        protected override string Endpoint => Endpoints.Purchases;

        public OrdersController(IRestClient restClient) 
            : base(restClient)
        {
        }

        [HttpPost]
        public async Task<ActionResult> Index(CreateOrderModel viewModel)
        {
            var order = new Order
            {
                ClientId = ParseInt(viewModel.ClientId),
                ProductId = ParseInt(viewModel.ProductId),
                Quantity = ParseInt(viewModel.Quantity),
                SKU = Guid.NewGuid().ToString()
            };

            await PostEntity(order);

            return await Index();
        }

        public override async Task<ActionResult> Create()
        {
            var clients = await GetEntities<IEnumerable<Client>>(Endpoints.Clients);
            var products = await GetEntities<IEnumerable<Product>>(Endpoints.Products);

            var createModel = new CreateOrderModel
            {
                Clients = GetDropDownList(clients),
                Products = GetDropDownList(products)
            };

            return View(createModel);
        }
    }
}