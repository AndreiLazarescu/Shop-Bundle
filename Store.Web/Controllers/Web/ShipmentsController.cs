using Store.Common.Constants;
using Store.Interfaces.Communication;
using Store.Model.Entities;
using Store.Web.Controllers.Base;
using Store.Web.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Store.Web.Controllers.Web
{
    /// <summary>
    /// The shipments controller.
    /// </summary>
    public class ShipmentsController : ShopItemsControllerBase<Shipment>
    {
        protected override string Endpoint => Endpoints.Orders;

        public ShipmentsController(IRestClient restClient)
            : base(restClient) { }

        [HttpPost]
        public async Task<ActionResult> Index(CreateShipmentModel viewModel)
        {
            var url = $"{Endpoints.Purchases}/Client/{ParseInt(viewModel.ClientId)}";
            var orders = await GetEntities<IEnumerable<Order>>(url);

            if (orders.Count() > 0)
            {
                var ordersGroups = orders.GroupBy(x => x.Product.Category.Name);
                foreach (var ordersGroup in ordersGroups)
                {
                    var shipment = new Shipment
                    {
                        Orders = new List<ShipmentOrder>()
                    };

                    foreach (var order in ordersGroup)
                    {
                        shipment.Orders.Add(new ShipmentOrder { OrderId = order.Id });
                    }

                    await PostEntity(shipment);
                }
            }

            return await Index();
        }

        public override async Task<ActionResult> Create()
        {
            var items = new List<Client>();
            var clients = await GetEntities<IEnumerable<Client>>(Endpoints.Clients);
            foreach (var client in clients)
            {
                var url = $"{Endpoints.Purchases}/Client/{client.Id}";
                var orders = await GetEntities<IEnumerable<Order>>(url);

                if (orders.Count() > 0)
                {
                    items.Add(client);
                }
            }

            var createModel = new CreateShipmentModel
            {
                Clients = GetDropDownList(items),
            };

            return View(createModel);
        }
    }
}