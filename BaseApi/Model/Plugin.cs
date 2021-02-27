using System;
using Swashbuckle.AspNetCore.Annotations;

namespace BaseApi.Model
{
    [SwaggerSchema(Description = "Plugin Model", Required = new[] {"Name", "Version", "Binary" })]
    public class Plugin
    {
        [SwaggerSchema("Name of the plugin")]
        public string Name { get; set; }

        [SwaggerSchema("Plugin version number")]
        public string Version { get; set; }

        [SwaggerSchema("Bytes of teh plugin binary")]
        public byte[] Binary { get; set; }

        public DateTime Date { get; set; }
    }
}
