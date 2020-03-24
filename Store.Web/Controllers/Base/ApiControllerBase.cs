using Store.Interfaces.Entity;
using Store.Interfaces.Repository;
using System.Threading.Tasks;
using System.Web.Http;

namespace Store.Web.Controllers.Base
{
    public abstract class ApiControllerBase<TEntity> : ApiController
        where TEntity : class, IEntity
    {
        protected readonly IRepository<TEntity> Repository;

        protected ApiControllerBase(IRepository<TEntity> repository)
        {
            Repository = repository;
        }

        public virtual IHttpActionResult Get()
        {
            var entities = Repository.GetEntities();

            return Ok(entities);
        }

        [AcceptVerbs("GET", "HEAD")]
        public virtual IHttpActionResult Page(int start, int end)
        {
            var entities = Repository.GetEntities(start, end);
            if (entities == null)
            {
                return NotFound();
            }

            return Ok(entities);
        }
        
        public virtual async Task<IHttpActionResult> Get(int id)
        {
            var entity = await Repository.GetEntity(x => x.Id == id);
            if (entity == null)
            {
                return NotFound();
            }

            return Ok(entity);
        }

        [HttpPost]
        public virtual async Task<IHttpActionResult> Post([FromBody]TEntity value)
        {
            if (value == null)
            {
                return BadRequest();
            }

            await Repository.Insert(value);

            return Ok(value);
        }

        [HttpPut]
        public async Task<IHttpActionResult> Put(int id, [FromBody]TEntity value)
        {
            if (value == null)
            {
                return BadRequest();
            }

            await Repository.Update(value);

            return Ok(value);
        }

        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var entity = await Repository.GetEntity(x => x.Id == id);
            if (entity == null)
            {
                return NotFound();
            }

            await Repository.Delete(entity);

            return Ok(entity);
        }
    }
}