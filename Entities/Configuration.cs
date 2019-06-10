using System;

namespace DotnetDemo.Entities
{
    public class Configuration
    {
        /// <summary>
        /// The id of the configuration, which is a Guid.
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// The name of the configuration
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The grid of statuses, stored as a six-by-six array.
        /// </summary>
        public ConfigurationStatus[,] Grid { get; set; }
    }

    public enum ConfigurationStatus
    {
        Default,
        Ok,
        Error
    }
}
