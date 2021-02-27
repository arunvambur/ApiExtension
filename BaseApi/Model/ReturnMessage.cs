using Swashbuckle.AspNetCore.Annotations;

namespace BaseApi.Model
{
    [SwaggerSchema("A generic model to return a simple message to the request")]
    public class ReturnMessage
    {
        [SwaggerSchema("Status of the operation")]
        public bool Success { get; set; }

        [SwaggerSchema("Message to be returned")]
        public string Message { get; set; }
    }
}
