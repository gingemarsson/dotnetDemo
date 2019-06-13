using DotnetDemo.Entities;
using System;
using System.Collections.Generic;

namespace DotnetDemo.Services
{
    public interface IConfigurationService
    {
        Configuration get(Guid id);
        IEnumerable<Configuration> getList();
        Configuration add(Configuration configuration);
        Configuration update(Configuration configuration);
        void delete(Guid id);
    }
}
