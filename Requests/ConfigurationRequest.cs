using DotnetDemo.Entities;
using System.ComponentModel.DataAnnotations;

namespace DotnetDemo.Requests
{

    public class ConfigurationRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [ArraySize6x6]
        public ConfigurationStatus[,] Grid { get; set; }
    }
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
