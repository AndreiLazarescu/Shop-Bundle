using Microsoft.EntityFrameworkCore;
using Store.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Store.DataAccessLayer.Repository
{
    public class OrderRepository : RepositoryBase<Order>
    {
        private string storedProcedure = "EXEC [dbo].[GetOrdersPage] @Start = {0}, @End = {1};";

        public OrderRepository(IStoreContext context) : base(context)
        {
            BaseIncludes = new Expression<Func<Order, object>>[]
            {
                x => x.Product,
                x => x.Product.Category,
                x => x.Client,
                x => x.Client.Address,
                x => x.Client.Address.City,
                x => x.Client.Address.City.State,
                x => x.Client.Address.City.State.Country
            };
        }

        public override IEnumerable<Order> GetEntities(int start, int end, Expression<Func<Order, bool>> where = null, params Expression<Func<Order, object>>[] includes)
        {
            var entities = Context.Orders.FromSql(storedProcedure, start, end).ToList();

            foreach (var entity in entities)
            {
                entity.Product = GetNoTrackingSet<Product>().FirstOrDefault(x => x.Id == entity.ProductId);
                entity.Client = GetNoTrackingSet<Client>().FirstOrDefault(x => x.Id == entity.ClientId);
            }

            return entities;
        }

        protected override Order ProcessEntity(Order entity)
        {
            if (entity.ClientId > 0)
            {
                entity.Client = null;
            }

            if (entity.ProductId > 0)
            {
                entity.Product = null;
            }

            return base.ProcessEntity(entity);
        }
    }
}
