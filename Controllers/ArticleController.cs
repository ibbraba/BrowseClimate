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
        [Authorize]
        public async Task<IActionResult> Create(Article article)
        {
      
            User user = await _userService.GetUser(3);


            try
            {
                article.CreatedBy= user.Id;
               

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



  

    }
}
