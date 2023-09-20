using BrowseClimate.Helpers;
using BrowseClimate.Models;
using BrowseClimate.Services.ArticleServices;
using BrowseClimate.Services.UserServices;

using Microsoft.AspNetCore.Mvc;

namespace BrowseClimate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ArticleController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private ArticleService _articleService;
        private UserService _userService;


        

        public ArticleController(IConfiguration configuration)
        {
            _articleService = new ArticleService();

            _configuration = configuration;
            _userService = new UserService(_configuration);
        }



        [HttpGet]
        [Route("Index")]
        public IActionResult Index()
        {
            //Main main = new();
            //   var data = main.ReadJSON();
            return Ok(DBHelper._cnn);
        }

        
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(Article article)
        {
      
            User user = await _userService.GetUser(3);

            try
            {

                article.Title = "Title";
                article.Content = "Content 222";
                article.Description = "desc";
                article.CreatedBy = user.Id;
                await _articleService.CreateArticle(article);
                
                return Ok(article);

            }catch (Exception ex)
            {
                throw new Exception( ex.Message);
            }
        }

        [HttpGet]
        [Route ("Get")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                Article article = await _articleService.GetArticle(id);
                return Ok(article);
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> Update (Article article)
        {

            
            article.Title = "Updated";
            Article articleToEdit = await _articleService.GetArticle(7);


            try
            {
                await _articleService.UpdateArticle(articleToEdit);   
                return Ok(article); 
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        [HttpPost]
        [Route("Delete")]

        public async Task<IActionResult> Delete(Article article)
        {
            try
            {
                await _articleService.DeleteArticle(article);
                return Ok("Article avec l'Id " + article.Id + " supprimé!");
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }


        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<Article> articles = await _articleService.GetAllArticles();
                return Ok(articles);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
