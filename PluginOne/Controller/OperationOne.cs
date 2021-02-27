using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using PluginOne.Model;


namespace PluginOne.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class OperationOne : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpGet]
        [SwaggerOperation(Summary = "OperationOne Get API", Description = "This method is from CustomerOne module")]
        [SwaggerResponse(200, "All is OK")]
        public IEnumerable<ModelOne> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new ModelOne
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [SwaggerOperation(Summary = "OperationOne Post API", Description = "This method is from CustomerOne module")]
        [SwaggerResponse(200, "All is OK")]
        [HttpPost]
        public ModelOne Post(ModelOne modelOne)
        {
            return new ModelOne { };
        }
    }

}
