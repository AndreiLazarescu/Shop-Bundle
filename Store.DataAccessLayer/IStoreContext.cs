using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Store.Model.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Store.DataAccessLayer
{
    public interface IStoreContext : IDisposable
    {
        DbSet<Product> Products { get; set; }
        DbSet<ProductCategory> ProductCategories { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<Shipment> Shipments { get; set; }
        DbSet<Client> Clients { get; set; }
        DbSet<Address> Addresses { get; set; }
        DbSet<City> Cities { get; set; }
        DbSet<State> States { get; set; }
        DbSet<Country> Countries { get; set; }

        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        DatabaseFacade Database { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
