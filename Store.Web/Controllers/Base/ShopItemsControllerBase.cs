using Store.Interfaces.Communication;
using Store.Interfaces.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Store.Web.Controllers.Base
{
    /// <summary>
    /// The shop items controller base.
    /// </summary>
    public abstract class ShopItemsControllerBase<TEntity> : ControllerBase<TEntity>
        where TEntity : class, IEntity
    {
        protected abstract string Endpoint { get; }

        protected ShopItemsControllerBase(IRestClient restClient)
            : base(restClient) { }

        public virtual async Task<ActionResult> Index()
        {
            var result = await GetEntities<IEnumerable<TEntity>>(Endpoint);

            return View(result);
        }

        public virtual async Task<ActionResult> Create()
        {
            return View();
        }

        public async Task<ActionResult> Delete([Bind(Include ="ID")] TEntity item)
        {
            var url = $"{Endpoint}/{item.Id}";
            var response = await RestClient.DeleteRequest(url).ExecuteAsync<TEntity>();

            return RedirectToAction("Index", GetType().Name.Replace("Controller", string.Empty));
        }

        protected Task<T> GetEntities<T>(string endpoint) where T: IEnumerable<IEntity>
        {
            var request = RestClient.GetRequest(endpoint);
            return request.ExecuteAsync<T>();
        }

        protected Task<T> GetEntity<T>(string endpoint, int id) where T : IEntity
        {
            var url = $"{endpoint}/{id}";
            var request = RestClient.GetRequest(url);
            return request.ExecuteAsync<T>();
        }

        protected Task<TEntity> PostEntity(TEntity entity)
        {
            var request = RestClient.PostRequest(Endpoint).AddContent(entity);

            return request.ExecuteAsync<TEntity>();
        }

        protected Task<TEntity> PutEntity(TEntity entity)
        {
            var request = RestClient.PutRequest(Endpoint).AddContent(entity);

            return request.ExecuteAsync<TEntity>();
        }
    }
}