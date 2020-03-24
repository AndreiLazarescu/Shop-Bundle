using Store.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Store.DataAccessLayer.Repository
{
    public class AddressRepository : RepositoryBase<Address>
    {
        public AddressRepository(IStoreContext context) : base(context)
        {
            BaseIncludes = new Expression<Func<Address, object>>[]
            {
                x => x.City,
                x => x.City.State,
                x => x.City.State.Country
            };
        }
    }
}
