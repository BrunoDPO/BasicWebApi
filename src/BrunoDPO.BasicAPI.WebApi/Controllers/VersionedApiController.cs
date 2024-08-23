using Microsoft.AspNetCore.Mvc;

namespace BrunoDPO.BasicAPI.WebApi.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public abstract class VersionedApiController : ControllerBase
    {
    }
}
