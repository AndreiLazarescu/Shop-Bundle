using Microsoft.EntityFrameworkCore;
using Store.Common.Constants;
using Store.DataAccessLayer;
using Store.Interfaces.Factory;
using Store.Interfaces.Service;

namespace Store.Common
{
    public class OptionsBuilderFactory : IFactory<DbContextOptionsBuilder<StoreContext>>
    {
        private readonly IConfigurationService configurationService;

        public OptionsBuilderFactory(IConfigurationService configurationService)
        {
            this.configurationService = configurationService;
        }

        public DbContextOptionsBuilder<StoreContext> GetInstance()
        {
            var builder = new DbContextOptionsBuilder<StoreContext>();

            builder.UseSqlServer(configurationService.ReadConnectionString(SettingKeys.ConnectionStringKey));

            return builder;
        }
    }
}