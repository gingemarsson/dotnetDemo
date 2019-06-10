using DotnetDemo.Entities;
using DotnetDemo.Repositories;
using System;
using System.Collections.Generic;

namespace DotnetDemo.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly IConfigurationRepository _repository;
        public ConfigurationService(IConfigurationRepository repository)
        {
            _repository = repository;
        }

        public Configuration getConfiguration(Guid id) { return _repository.getConfiguration(id); }

        public IEnumerable<Configuration> getConfigurationList() { return _repository.getConfigurationList(); }

        public Configuration addConfiguration(Configuration configuration) { return _repository.saveConfiguration(configuration); }

        public Configuration updateConfiguration(Configuration configuration) { return _repository.updateConfiguration(configuration); }
    }

    public interface IConfigurationService
    {
        Configuration getConfiguration(Guid id);
        IEnumerable<Configuration> getConfigurationList();
        Configuration addConfiguration(Configuration configuration);
        Configuration updateConfiguration(Configuration configuration);
    }
}
