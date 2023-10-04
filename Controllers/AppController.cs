using BrowseClimate.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.Cors;

namespace BrowseClimate.Controllers
{
  
    [Route("api/[controller]")]
    [ApiController]
   // [EnableCors(origins: "http://localhost:5173/", headers: "*", methods: "*")]
    public class AppController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AppController(IConfiguration configuration)
        {
            _configuration = configuration;
        }



        [HttpGet]
        [Route ("Index")]
        [Authorize]
        public IActionResult Index()
        {
         //Main main = new();
         //   var data = main.ReadJSON();
            return Ok(DBHelper._cnn);
        }

        [HttpGet]
        [Route("City")]
        public string City(int id)
        {

            return "Temperature for a specific city + articles  + stories + stats? ";

        }


        [HttpGet]
        [Route("Articles")]
        public string Artcles()
        {
            return "All articles";
        }

        [HttpGet]
        [Route("Article")]
        public string Article (int id) {

            return " single Articles";        
        }
        [HttpGet]
        [Route("Stories")]
        
        public string Stories() {

            return "all Stories";
        }


        [HttpGet]
        [Route("Story")]
        public string Story(int id) {

            return "Single story";
        }

             


    }
}
