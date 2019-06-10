using System;
using System.Collections.Generic;
using System.Linq;
using DotnetDemo.Entities;
using DotnetDemo.Requests;
using DotnetDemo.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotnetDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigurationController : ControllerBase
    {
        private readonly IConfigurationService _service;

        public ConfigurationController(IConfigurationService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get a list of all saved configurations.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<Configuration>> GetList()
        {
            return _service.getConfigurationList().ToList();
        }

        /// <summary>
        /// Get the specified configuration.
        /// </summary>
        /// <param name="id">The id of the configuration to fetch.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<Configuration> Get([FromRoute] Guid id)
        {
            return _service.getConfiguration(id);
        }

        /// <summary>
        /// Create a new configuration.
        /// </summary>
        /// <param name="configurationRequest">The configuration to save.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<Configuration> Save([FromBody] ConfigurationRequest configurationRequest)
        {
            var configuration = new Configuration
            {
                Id = Guid.NewGuid(),
                Name = configurationRequest.Name,
                Grid = configurationRequest.Grid
            };
            return _service.addConfiguration(configuration);
        }

        /// <summary>
        /// Update the specified configuration.
        /// </summary>
        /// <param name="id">The configuration to update.</param>
        /// <param name="configurationRequest">The enw content.</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ActionResult<Configuration> Update([FromRoute] Guid id, [FromBody]ConfigurationRequest configurationRequest)
        {
            var configuration = new Configuration
            {
                Id = id,
                Name = configurationRequest.Name,
                Grid = configurationRequest.Grid
            };
            return _service.updateConfiguration(configuration);
        }

        /// <summary>
        /// Delete the specified configuration.
        /// </summary>
        /// <param name="id">The configuration to delete.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] Guid id)
        {
            _service.deleteConfiguration(id);
            return Ok();
        }
    }
}