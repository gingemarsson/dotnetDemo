using DotnetDemo.Entities;
using System.ComponentModel.DataAnnotations;

namespace DotnetDemo.Requests
{

    public class ConfigurationRequest
    {
        /// <summary>
        /// The name of the configuration
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// The grid of statuses, stored as a six-by-six array.
        /// </summary>
        [Required]
        [ArraySize6x6]
        public ConfigurationStatus[,] Grid { get; set; }
    }

    /// <summary>
    /// This validation attribute can be used to check that the grid in the
    /// above request is six-by-six.
    /// </summary>
    public class ArraySize6x6 : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return value.GetType().IsArray
                && (value as ConfigurationStatus[,]).Rank == 2
                && (value as ConfigurationStatus[,]).GetLength(0) == 6
                && (value as ConfigurationStatus[,]).GetLength(1) == 6;
        }
    }
}
