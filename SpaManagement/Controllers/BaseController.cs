using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SpaManagement.Controllers
{
    [Authorize]
    [Route("api/[controller]")] 
    [ApiController]
    public class BaseController : ControllerBase
    {

    }
}
