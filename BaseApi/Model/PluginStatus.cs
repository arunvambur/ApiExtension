using Swashbuckle.AspNetCore.Annotations;

namespace BaseApi.Model
{
    [SwaggerSchema("Status of the plugin to activate/deactivate")]
    public class PluginStatus
    {
        [SwaggerSchema("Name of the plugin")]
        public string Name { get; set; }

        [SwaggerSchema("Pluting version number")]
        public string Version { get; set; }

        [SwaggerSchema("Activate the plugin and load into memory")]
        public bool Activate { get; set; }
    }
}
