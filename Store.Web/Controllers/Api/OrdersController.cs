using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Store.Interfaces.Repository;
using Store.Model.Entities;
using Store.Web.Controllers.Base;

namespace Store.Web.Controllers.Api
{
    public class OrdersController : ApiControllerBase<Shipment>
    {
        public OrdersController(IRepository<Shipment> repository)
            : base(repository) { }

        [HttpGet]
        public override IHttpActionResult Get()
        {
            var entities = Repository.GetEntities();

            return Ok(ProcessShipments(entities));
        }

        [AcceptVerbs("GET", "HEAD")]
        public override IHttpActionResult Page(int start, int end)
        {
            var entities = Repository.GetEntities(start, end);
            if (entities == null)
            {
                return NotFound();
            }

            return Ok(ProcessShipments(entities));
        }

        private IEnumerable<Shipment> ProcessShipments(IEnumerable<Shipment> entities)
        {
            foreach (var entity in entities)
            {
                var orderShipment = entity.Orders.FirstOrDefault(x => x.Order != null);
                if (orderShipment != null)
                {
                    entity.FirstName = orderShipment.Order.Client.FirstName;
                    entity.LastName = orderShipment.Order.Client.LastName;
                    entity.Address = orderShipment.Order.Client.Address.Street;
                    entity.City = orderShipment.Order.Client.Address.City.Name;
                    entity.State = orderShipment.Order.Client.Address.City.State.Name;
                    entity.Country = orderShipment.Order.Client.Address.City.State.Country.Name;
                }

                foreach (var order in entity.Orders)
                {
                    entity.Products.Add(new ShipmentProduct
                    {
                        SKU = order.Order.SKU,
                        Quantity = order.Order.Quantity
                    });
                }

                entity.Orders = null;
            }

            return entities;
        }
    }
}