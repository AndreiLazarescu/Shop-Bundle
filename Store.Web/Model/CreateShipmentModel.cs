using System.Collections.Generic;
using System.Web.Mvc;

namespace Store.Web.Model
{
    public class CreateShipmentModel
    {
        public string ClientId { get; set; }
        public IEnumerable<SelectListItem> Clients { get; set; }
    }
}