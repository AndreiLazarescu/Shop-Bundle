using Store.Interfaces.Repository;
using Store.Model.Entities;
using Store.Web.Controllers.Base;
using System.Threading.Tasks;
using System.Web.Http;

namespace Store.Web.Controllers.Api
{
    public class PurchasesController : ApiControllerBase<Order>
    {
        public PurchasesController(IRepository<Order> repository)
            : base(repository) { }


        [HttpGet]
        [ActionName("Client")]
        public IHttpActionResult Client(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var entity = Repository.GetEntities(x => x.ClientId == id && (x.Shipments == null || x.Shipments.Count == 0));
            if (entity == null)
            {
                return NotFound();
            }

            return Ok(entity);
        }

        [HttpGet]
        [Route("api/Purchases/{id}")]
        public async override Task<IHttpActionResult> Get(int id)
        {
            return await base.Get(id);
        }
    }
}