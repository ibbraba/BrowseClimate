using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BrowseClimate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        [Route("MyPage")]
        public string MyPage(int id)
        {
            return "Profile page";
        }

    }
}
