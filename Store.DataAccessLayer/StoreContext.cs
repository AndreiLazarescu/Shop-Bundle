using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Store.Interfaces.Factory;
using Store.Model.Entities;

namespace Store.DataAccessLayer
{
    public class StoreContext : DbContext, IStoreContext
    {
        private static string connection = "Store";

        public StoreContext(IFactory<DbContextOptionsBuilder<StoreContext>> factory) 
            : this(factory.GetInstance().Options) { }

        public StoreContext(DbContextOptions<StoreContext> options) 
            : base(options) { }


        public static void Migrate(string connectionSetting)
        {
            connection = connectionSetting;

            var builder = new DbContextOptionsBuilder<StoreContext>();

            builder.UseSqlServer(connectionSetting);

            using (var context = new StoreContext(builder.Options))
            {
                context.Database.Migrate();

                Seed(context);

                context.SaveChanges();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ShipmentOrder>().
                HasKey(so => new { so.ShipmentId, so.OrderId });
            modelBuilder.Entity<ShipmentOrder>()
                .HasOne(s => s.Shipment)
                .WithMany(o => o.Orders)
                .HasForeignKey(x => x.ShipmentId);
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Country> Countries { get; set; }


        private static void Seed(StoreContext context)
        {

            if (!context.Products.Any())
            {
                context.Products.Add(new Product
                {
                    Name = "Some TV",
                    Description = "Amazing TV",
                    Category = new ProductCategory { Name = "Electronics" }
                });

                context.Products.Add(new Product
                {
                    Name = "Chair",
                    Description = "Great chair",
                    Category = new ProductCategory { Name = "Furniture" }
                });
            }

            if (!context.Clients.Any())
            {
                var address = new Address
                {
                    Street = "16 Avenue",
                    City = new City
                    {
                        Name = "New York",
                        State = new State
                        {
                            Name = "New York",
                            Country = new Country { Name = "United States" }
                        }
                    }
                };

                context.Clients.Add(new Client { FirstName = "John", LastName = "Doe", Address = address});
                context.Clients.Add(new Client { FirstName = "Jane", LastName = "Doe", Address = address});
            }
        }
    }
}
