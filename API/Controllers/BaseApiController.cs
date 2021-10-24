using API.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    //ActionFilter
    [ServiceFilter(typeof(LogUserActivity))]
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {

    }
}