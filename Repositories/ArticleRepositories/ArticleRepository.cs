using BrowseClimate.Helpers;
using BrowseClimate.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BrowseClimate.Repositories.ArticleRepositories
{
    public class ArticleRepository : IArticleRepository 
    {
    
        public async Task<int> CreateArticle(Article article)
        {
            int createdBy = article.CreatedBy;
            string title = article.Title;
            string description = article.Description;
            string content = article.Content;
            DateTime createdAt = article.CreatedAt;
            DateTime updatedAt = article.UpdatedAt;


            using (IDbConnection db = DBHelper.connectToDB())
            {



                var articleId = await db.QuerySingleOrDefaultAsync<int>("dbo.SpArticle_CreateArticle", new { createdBy, title, description, content, createdAt, updatedAt }, commandType: CommandType.StoredProcedure); ;
                return articleId;

            }
        }

        public async Task DeleteArticle(int id)
        {
            using (IDbConnection db = DBHelper.connectToDB())
            {
                var output = await db.ExecuteAsync("dbo.SpArticle_DeleteArticle", new { id }, commandType: CommandType.StoredProcedure);

            }
        }

        public async Task<List<Article>> GetAllArticles()
        {
            using (IDbConnection db = DBHelper.connectToDB())
            {

                var output = await db.QueryAsync<Article>("dbo.SpArticle_GetAllArticles", commandType: CommandType.StoredProcedure);
                return output.ToList();

            }
        }

        public async Task<Article> GetArticle(int id)
        {
            using (IDbConnection db = DBHelper.connectToDB())
            {

                var output = await db.QuerySingleOrDefaultAsync<Article>("dbo.SpArticle_FindArticleWithId", new { id }, commandType: CommandType.StoredProcedure);
                return output;
            }

        }

        public async Task<List<Article>> GetArticlesLikedByUser(int userId)
        {
            using (IDbConnection db = DBHelper.connectToDB())
            {

                List<Article> articles = new();

                var output = await db.QueryAsync<int>("dbo.SpArticleLike_GetUserLikes", new { userId }, commandType: CommandType.StoredProcedure);
                
                foreach(int id in output)
                {
                    var article = await db.QuerySingleAsync<Article>("dbo.SpArticle_FindArticleWithId", new { id }, commandType: CommandType.StoredProcedure) ;
                    articles.Add(article);

                }

                return articles; 
                    
            }

        }



        public async Task<List<User>> GetLikesOnArticle(int articleId)
        {
            using (IDbConnection db = DBHelper.connectToDB())
            {
                List<User> users = new();

                var userIds = await db.QueryAsync<int>("dbo.SpArticleLike_GetLikesOnArticles", new { articleId }, commandType: CommandType.StoredProcedure);
                userIds = userIds.ToList();


                foreach (int id in userIds)
                {
                    var output = await db.QuerySingleOrDefaultAsync<User>("dbo.SpUsers_FindUserWithId", new { id }, commandType: CommandType.StoredProcedure);
                    users.Add(output);
                }

                return users;
            }
        }

        public async Task<List<Article>> GetTopArticles()
        {
            using (IDbConnection db = DBHelper.connectToDB())
            {

                var output = await db.QueryAsync<Article>("dbo.SpArticle_FindTopArticles", commandType: CommandType.StoredProcedure);
                return output.ToList();
            }
        }

        public async Task<List<Article>> GetUserArticles(int id)
        {
            using (IDbConnection db = DBHelper.connectToDB())
            {

                var output = await db.QueryAsync<Article>("dbo.SpArticle_GetUserArticles", new { id }, commandType: CommandType.StoredProcedure);

                return output.ToList();
            }
        }

        public async Task UpdateArticle(Article article)
        {

            int id = article.Id;
            string title = article.Title;
            string description = article.Description;
            string content = article.Content;


            using (IDbConnection db = DBHelper.connectToDB())
            {
                var output = await db.ExecuteAsync("dbo.SpArticle_EditArticle", new { id, title, description, content }, commandType: CommandType.StoredProcedure);
            }
        }


        
        public async Task AddLike(int articleId, int userId)
        {
            

            using (IDbConnection db = DBHelper.connectToDB())
            {
                var output = await db.ExecuteAsync("dbo.SpArticle_AddLike", new { articleId, userId}, commandType: CommandType.StoredProcedure);
            }
        }


        public async Task RemoveLike(int articleId, int userId)
        {


            using (IDbConnection db = DBHelper.connectToDB())
            {
                var output = await db.ExecuteAsync("dbo.SpArticle_RemoveLike", new { articleId, userId }, commandType: CommandType.StoredProcedure);
            }
        }

    }
}
