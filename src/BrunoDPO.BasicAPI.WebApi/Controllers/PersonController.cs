using Asp.Versioning;
using BrunoDPO.BasicAPI.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;

namespace BrunoDPO.BasicAPI.WebApi.Controllers
{
    [ApiVersion("1")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public class PersonController : ApiControllerBase
    {
        private readonly ILogger<PersonController> _logger;

        public PersonController(ILogger<PersonController> logger)
        {
            _logger = logger;
        }

        [HttpPost(Name = "PostPerson")]
        [SwaggerOperation(Summary = "Includes a new Person", Description = "Persists a new Person in the database")]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<Person> PostPersonAsync([FromBody]Person person, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Creating new Person: {@Person}", person);
            return Task.FromResult(person);
        }
    }
}
