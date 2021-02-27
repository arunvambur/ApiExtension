using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Loader;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Swashbuckle.AspNetCore.Annotations;
using BaseApi.Model;


namespace BaseApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PluginStatusController : ControllerBase
    {
        private readonly string[] _plugins = new[]
      {
            "Plugin1", "Plugin2", "Plugin3"
        };

        private readonly ApplicationPartManager _partManager;
        private readonly IHostingEnvironment _hostingEnvironment;

        public PluginStatusController(
            ApplicationPartManager partManager,
            IHostingEnvironment env)
        {
            _partManager = partManager;
            _hostingEnvironment = env;
        }


        [HttpGet]
        [SwaggerOperation(Summary = "Get the plugin status", Description = "This method returns the plugin status available in the server")]
        [SwaggerResponse(200, "All is OK")]
        public IEnumerable<PluginStatus> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new PluginStatus
            {
                Name = _plugins[rng.Next(_plugins.Length)],
                Version = rng.Next(10).ToString(),
                Activate = true
            })
            .ToArray();
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Upload a plugin to activate", Description = "Upload a plugin to activate")]
        [SwaggerResponse(200, "All is OK")]
        public PluginStatus Post([FromBody] PluginStatus plugin)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Plugin");
            if (Directory.Exists(path))
            {

                string assemblyPath = Path.Combine(path, plugin.Name + ".dll");

                var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(assemblyPath);
                if (assembly != null)
                {
                    _partManager.ApplicationParts.Add(new AssemblyPart(assembly));
                    // Notify change
                    ActionDescriptorChangeProvider.Instance.HasChanged = true;
                    ActionDescriptorChangeProvider.Instance.TokenSource.Cancel();
                }
            }

            return new PluginStatus
            {
                Name = "Plugin1",
                Version = "1"
            };
        }
    }

}
