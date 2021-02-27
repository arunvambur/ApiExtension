using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using BaseApi.Model;


namespace BaseApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PluginController : ControllerBase
    {
        private readonly string[] _plugins = new[]
        {
            "Plugin1", "Plugin2", "Plugin3"
        };


        [HttpGet]
        [SwaggerOperation(Summary = "Get the plugins", Description = "This method returns the plugins available in the server")]
        [SwaggerResponse(200, "All is OK")]
        public IEnumerable<Plugin> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new Plugin
            {
                Date = DateTime.Now.AddDays(index),
                Name = _plugins[rng.Next(_plugins.Length)],
                Version = rng.Next(10).ToString()
            })
            .ToArray();
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Upload a plugin to the server", Description = "This method upload the plugin(dll file) to the server")]
        [SwaggerResponse(200, "All is OK")]
        public Plugin Post(Plugin plugin)
        {
            return new Plugin
            {
                Name = "Plugin1",
                Date = DateTime.Now,
                Version = "1"
            };
        }

        [HttpPut]
        [SwaggerOperation(Summary = "Change existing plugin to the server", Description = "This method upload the existing plugin(dll file) to the server")]
        [SwaggerResponse(200, "All is OK")]
        public Plugin Put(Plugin plugin)
        {
            return new Plugin
            {
                Name = "Plugin1",
                Date = DateTime.Now,
                Version = "1"
            };
        }

        [HttpDelete]
        [SwaggerOperation(Summary = "Deletes the plugin from the server", Description = "This method deletes the plugin from server")]
        [SwaggerResponse(200, "All is OK")]
        public ReturnMessage Delete(Plugin plugin)
        {
            return new ReturnMessage
            {
                Success = true,
                Message = "The plugin deleted successfully from the server"
            };
        }

    }

}
