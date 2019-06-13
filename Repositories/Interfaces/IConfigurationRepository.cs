using DotnetDemo.Entities;
using System;
using System.Collections.Generic;

namespace DotnetDemo.Repositories
{
    public interface IConfigurationRepository
    {
        Configuration get(Guid id);
        IEnumerable<Configuration> getList();
        Configuration save(Configuration configuration);
        Configuration update(Configuration configuration);
        void delete(Guid id);
    }
}
