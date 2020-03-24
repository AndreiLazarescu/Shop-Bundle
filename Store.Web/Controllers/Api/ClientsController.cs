
using Store.Interfaces.Repository;
using Store.Model.Entities;
using Store.Web.Controllers.Base;

namespace Store.Web.Controllers.Api
{
    public class ClientsController : ApiControllerBase<Client>
    {
        public ClientsController(IRepository<Client> repository)
            : base(repository) { }

    }
}