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

        public Configuration get(Guid id) { return _repository.get(id); }

        public IEnumerable<Configuration> getList() { return _repository.getList(); }

        public Configuration add(Configuration configuration) { return _repository.save(configuration); }

        public Configuration update(Configuration configuration) { return _repository.update(configuration); }
        public void delete(Guid id) { _repository.delete(id); }
    }
}
