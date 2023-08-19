using BrowseClimate.Models;

namespace BrowseClimate.Repositories.CommentRepositories
{
    public interface ICommentRepository
    {

        Task CreateComment(Comment comment);
        Task<Comment> GetComment(int id);
        Task<List<Comment>> GetAllCommentsForArticle(int articleId);
        Task UpdateComment(Comment comment); 
        Task DeleteComment(int id);    



    }
}
