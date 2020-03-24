using System.Collections.Generic;
using System.Web.Mvc;

namespace Store.Web.Model
{
    public class CreateProductModel
    {
        public string Product { get; set; }
        public string Description { get; set; }
        public string CategoryId { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}