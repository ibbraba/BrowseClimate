using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BrowseClimate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppController : ControllerBase
    {
        [HttpGet]
        [Route ("Index")]
        public string Index()
        {
            return DateTime.Now.ToString(); 
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
