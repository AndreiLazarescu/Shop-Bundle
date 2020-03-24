using Store.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Store.DataAccessLayer.Repository
{
    public class ClientRepository : RepositoryBase<Client>
    {
        public ClientRepository(IStoreContext context) : base(context)
        {
            BaseIncludes = new Expression<Func<Client, object>>[]
            {
                x => x.Address.City,
                x => x.Address.City.State,
                x => x.Address.City.State.Country
            };
        }

        protected override Client ProcessEntity(Client entity)
        {
            if (entity.AddressId > 0)
            {
                entity.Address = null;
            }

            return base.ProcessEntity(entity);
        }
    }
}
