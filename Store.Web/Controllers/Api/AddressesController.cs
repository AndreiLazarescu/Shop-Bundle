using Store.Interfaces.Repository;
using Store.Model.Entities;
using Store.Web.Controllers.Base;

namespace Store.Web.Controllers
{
    /// <summary>
    /// The addresses controller.
    /// </summary>
    public class AddressesController : ApiControllerBase<Address>
    {
        public AddressesController(IRepository<Address> repository)
            : base(repository) { }

        
    }
}
