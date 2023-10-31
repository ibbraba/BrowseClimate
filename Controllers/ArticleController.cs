﻿using BrowseClimate.Helpers;
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


        [HttpDelete]
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

                return BadRequest(ex);
            }

        }



        [HttpGet]
        [Route("GetCityArticle")]
        public async Task<IActionResult> GetCityArticles(int cityId)
        {
            try
            {
                List<Article> articles = await _articleService.GetAllArticles();
                articles = articles.Where(x => x.CityId == cityId).ToList();
                return Ok(articles);
            }
            catch (Exception)
            {

                throw;
            }

        }




        [HttpPost]
        [Route("GetArticlesLikedByUser")]
        public async Task<IActionResult> GetArticlesLikedByUser(int userId)
        {
            try
            {

                List<Article> articles = await _articleService.GetArticlesLikedByUser(userId);
                return Ok(articles);


            }
            catch (Exception ex)
            {
                return BadRequest(ex);
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

        [HttpPost]
        [Route("AddLike")]
        public async Task<IActionResult> AddLike(int articleId, int userId)
        {

            try
            {
                await _articleService.AddLike(articleId, userId);
                return Ok();

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);

            }


        }

        [HttpPost]
        [Route("RemoveLike")]
        public async Task<IActionResult> RemoveLike(int articleId, int userId)
        {
            try
            {
                await _articleService.RemoveLike(articleId, userId);
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest();
            }
        }


    }
}
