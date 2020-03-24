using Store.Interfaces.Service;
using System;
using System.Configuration;

namespace Store.Common.Service
{
    public class ConfigurationService : IConfigurationService
    {
        public int ReadInt(string key)
        {
            var result = 0;
            var appSettings = ConfigurationManager.AppSettings;
            string setting = appSettings[key];

            int.TryParse(setting, out result);

            return result;
        }

        public string ReadString(string key)
        {
            var appSettings = ConfigurationManager.AppSettings;
            string setting = appSettings[key];

            return setting;
        }

        public string ReadConnectionString(string key)
        {
            var result = string.Empty;
            var connections = ConfigurationManager.ConnectionStrings;

            foreach (ConnectionStringSettings connection in connections)
            {
                if (string.Equals(key, connection.Name, StringComparison.InvariantCultureIgnoreCase))
                {
                    result = connection.ConnectionString;
                }
            }

            return result;
        }
    }
}
