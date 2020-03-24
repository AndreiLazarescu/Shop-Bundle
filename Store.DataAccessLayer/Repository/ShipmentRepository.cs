using Microsoft.EntityFrameworkCore;
using Store.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Store.DataAccessLayer.Repository
{
    public class ShipmentRepository : RepositoryBase<Shipment>
    {
        private Expression<Func<Order, object>>[] ExtraIncludes { get; set; }

        public ShipmentRepository(IStoreContext context) : base(context)
        {
            BaseIncludes = new Expression<Func<Shipment, object>>[] 
            {
                x => x.Orders
            };

            ExtraIncludes = new Expression<Func<Order, object>>[]
            {
                x => x.Client,
                x => x.Client.Address,
                x => x.Client.Address.City,
                x => x.Client.Address.City.State,
                x => x.Client.Address.City.State.Country
            };
        }

        public override IEnumerable<Shipment> GetEntities(Expression<Func<Shipment, bool>> where = null, params Expression<Func<Shipment, object>>[] includes)
        {
            IEnumerable<Shipment> entities = null;
            var dbSet = GetTrackingSet<Shipment>();

            dbSet = ApplyIncludes(dbSet, includes);
            dbSet = IncludeOrders(dbSet);

            if (where != null)
            {
                entities = dbSet.Where(where);
            }
            else
            {
                entities = dbSet;
            }

            return entities;
        }

        public override Task<Shipment> GetEntity(Expression<Func<Shipment, bool>> where, params Expression<Func<Shipment, object>>[] includes)
        {
            var dbSet = GetTrackingSet<Shipment>();

            dbSet = ApplyIncludes(dbSet, includes);
            dbSet = IncludeOrders(dbSet);

            return dbSet.FirstOrDefaultAsync(where);
        }

        private IQueryable<Shipment> IncludeOrders(IQueryable<Shipment> dbSet)
        {
            var ordersDbSet = Context.Orders.AsQueryable();

            ordersDbSet = ApplyExtraIncludes(ordersDbSet, ExtraIncludes);

            foreach (var shipment in dbSet)
            {
                foreach (var order in shipment.Orders)
                {
                    order.Order = ordersDbSet.FirstOrDefault(x => x.Id == order.OrderId);
                }
            }

            return dbSet;
        }

        private IQueryable<Order> ApplyExtraIncludes(IQueryable<Order> queryable, params Expression<Func<Order, object>>[] includes)
        {
            if (!includes.Any()) return queryable;

            foreach (var include in includes)
            {
                queryable = queryable.Include(include);
            }

            return queryable;
        }
    }
}
