using BrowseClimate.Helpers;
using BrowseClimate.Models;
using Dapper;
using System.Data;

namespace BrowseClimate.Repositories.CommentRepositories
{
    public class CommentRepository : ICommentRepository
    {
        public async Task CreateComment(Comment comment)
        {
            int userId = comment.CreatedBy.Id;
            int articleId = comment.Article.Id;
            DateTime createdAt = comment.CreatedAt; 
            string content = comment.Content;
            

            using (IDbConnection db = DBHelper.connectToDB())
            {
                var output = await db.ExecuteAsync("dbo.SpComment_CreateComment", new { userId, articleId, createdAt, content }, commandType: CommandType.StoredProcedure); 
                
            }
        }

        public async Task DeleteComment(int id)
        {
           
                
            using (IDbConnection db = DBHelper.connectToDB())
            {
                var output = await db.ExecuteAsync("dbo.SpComment_DeleteComment", new { id }, commandType: CommandType.StoredProcedure);

            }
        }

        public async Task<List<Comment>> GetAllCommentsForArticle(int articleId)
        {


            using (IDbConnection db = DBHelper.connectToDB())
            {
                var output = await db.QueryAsync<Comment>("dbo.SpComment_GetAllCommentsForArticle", new { articleId}, commandType: CommandType.StoredProcedure);
                return output.ToList(); 

            }
          
        }

        public async Task<Comment> GetComment(int id)
        {
            using (IDbConnection db = DBHelper.connectToDB())
            {
                var output = await db.QuerySingleOrDefaultAsync<Comment>("dbo.SpComment_DeleteComment", new { id }, commandType: CommandType.StoredProcedure);
                return output;

            }
        }

        public async Task UpdateComment(Comment comment)
        {
            int id = comment.Id;
            DateTime updatedAt = comment.UpdatedAt; 
            string content = comment.Content;   

            using (IDbConnection db = DBHelper.connectToDB())
            {
                var output = await db.ExecuteAsync("SpComment_EditComment", new { id, updatedAt, content }, commandType: CommandType.StoredProcedure);
            }

        }
    }
}
