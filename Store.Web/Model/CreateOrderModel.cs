using System.Collections.Generic;
using System.Web.Mvc;

namespace Store.Web.Model
{
    public class CreateOrderModel
    {
        public string ClientId { get; set; }
        public IEnumerable<SelectListItem> Clients { get; set; }

        public string ProductId { get; set; }
        public IEnumerable<SelectListItem> Products { get; set; }

        public string Quantity { get; set; }
    }
}