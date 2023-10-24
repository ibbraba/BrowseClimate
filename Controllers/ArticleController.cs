using BrowseClimate.Helpers;
using BrowseClimate.Models;
using BrowseClimate.Services.ArticleServices;
using BrowseClimate.Services.CommentService;
using BrowseClimate.Services.UserServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace BrowseClimate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ArticleController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private ArticleService _articleService;
        private readonly CommentService _commentService;
        private UserService _userService;


        

        public ArticleController(IConfiguration configuration)
        {
            _articleService = new ArticleService();
            _commentService = new CommentService();

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
      
            try
            {
          

                int articleID = await _articleService.CreateArticle(article);
                article.Id = articleID;
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
                if (article != null)
                {
                    List<Comment> comments = await _commentService.GetAllCommentsForArticle(article.Id);
                    article.Comments = comments;
                    return Ok(article);
                }
                else return BadRequest("Article not found");
            
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        [HttpPost]
        [Route("Update")]
        [Authorize]
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
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _articleService.DeleteArticle(id);
                return Ok("Article avec l'Id " + id + " supprimé!");
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
                foreach (Article article in articles)
                {
                     List<Comment> comments =  await _commentService.GetAllCommentsForArticle(article.Id);
                     article.Comments = comments;

                }

                return Ok(articles);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        [HttpGet]
        [Route("GetUserArticle")]
        public async Task<IActionResult> GetUsersArticles(int id)
        {
            try
            {
                List<Article> articles = await _articleService.GetUserArticles(id);
                return Ok(articles);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpPut]
        [Route("Comment/Update")]
        [Authorize]
        public async Task<IActionResult>  UpdateComment(Comment comment)
        {
            try
            {
                await _commentService.UpdateComment(comment);
                return Ok("Comment updated with success");
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
      
        }

        [HttpDelete]
        [Route("Comment/Delete")]
        [Authorize]
        public async Task<IActionResult> DeleteComment(int id)
        {
            try
            {
                await _commentService.DeleteComment(id);
                return Ok("Comment deleted with success");
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("Comment/Create")]
        [Authorize]
        public async Task<IActionResult> CreateComment(Comment comment, int articleId)
        {
            try
            {
                await _commentService.CreateComment(comment, articleId);
                return Ok("Comment created with success!");
                
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }


        [HttpGet]
        [Route("GetDiscoverArticles")]
   
        public async Task<IActionResult> GetDiscoverArticles(int userId)
        {
            try
            {
                List<Article> articles = await _articleService.GetDiscoverArticles(userId); 
                foreach (Article article in articles)
                {
                    article.Timestamp = (int)article.CreatedAt.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
                }
                return Ok(articles);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }



        }




  

    }
}
