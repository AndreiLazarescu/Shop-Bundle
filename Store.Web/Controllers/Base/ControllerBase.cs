using Store.Interfaces.Communication;
using Store.Interfaces.Entity;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Store.Web.Controllers.Base
{
    public abstract class ControllerBase<TEntity> : Controller
        where TEntity : class, IEntity
    {
        protected readonly IRestClient RestClient;

        public ControllerBase(IRestClient restClient)
        {
            RestClient = restClient;
        }

        protected IEnumerable<SelectListItem> GetDropDownList<T>(IEnumerable<T> items) where T : IEntity
        {
            var listItems = new List<SelectListItem>();
            foreach (var item in items)
            {
                listItems.Add(new SelectListItem { Text = item.ToString(), Value = item.Id.ToString() });
            }

            return listItems;
        }

        protected int ParseInt(string value)
        {
            if (int.TryParse(value, out int result))
            {
                return result;
            }

            return 0;
        }
    }
}