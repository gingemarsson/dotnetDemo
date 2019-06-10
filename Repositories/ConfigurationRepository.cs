using DotnetDemo.Entities;
using System;
using System.Collections.Generic;

namespace DotnetDemo.Repositories
{
    /// <summary>
    /// For now, store the configurations in a Dictionary. For a more permanent 
    /// solution it would be better to use a real database.
    /// </summary>
    public class ConfigurationRepository : IConfigurationRepository
    {
        private Dictionary<Guid, Configuration> database;

        public ConfigurationRepository()
        {
            database = new Dictionary<Guid, Configuration>();
        }

        public Configuration getConfiguration(Guid id)
        {
            return database[id];
        }

        public IEnumerable<Configuration> getConfigurationList()
        {
            return database.Values;
        }

        public Configuration saveConfiguration(Configuration configuration)
        {
            database.Add(configuration.Id, configuration);
            return configuration;
        }

        public Configuration updateConfiguration(Configuration configuration)
        {
            if (database.ContainsKey(configuration.Id))
            {
                database[configuration.Id] = configuration;
                return configuration;
            }
            else { return null; }
        }
    }

    public interface IConfigurationRepository
    {
        Configuration getConfiguration(Guid id);
        IEnumerable<Configuration> getConfigurationList();
        Configuration saveConfiguration(Configuration configuration);
        Configuration updateConfiguration(Configuration configuration);

    }
}
