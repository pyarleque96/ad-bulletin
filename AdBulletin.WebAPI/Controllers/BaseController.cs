using AdBulletin.WebAPI.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdBulletin.WebAPI.Controllers
{
    [Authorize]
    [ServiceFilter(typeof(ApiExceptionFilter))]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BaseController : ControllerBase
    {
        public BaseController()
        {
        }
    }
}