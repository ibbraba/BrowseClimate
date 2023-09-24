using BrowseClimate.Helpers;
using BrowseClimate.Models;
using Dapper;
using System.Data;

namespace BrowseClimate.Repositories.ArticleRepositories
{
    public class ArticleRepository : IArticleRepository
    {
    
        public async Task CreateArticle(Article article)
        {
            int createdBy = article.CreatedBy;
            string title = article.Title;
            string description = article.Description;
            string content = article.Content;


            using (IDbConnection db = DBHelper.connectToDB())
            {



                var output = await db.ExecuteAsync("dbo.SpArticle_CreateArticle", new { createdBy, title, description, content }, commandType: CommandType.StoredProcedure);
                    
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



    }
}
