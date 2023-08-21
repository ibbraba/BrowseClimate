using BrowseClimate.Models;

namespace BrowseClimate.Services.CommentService
{
    public interface ICommentService
    {
         Task CreateComment( Comment comment);

        Task<Comment> GetComment(int id);

        Task UpdateComment (Comment comment );

        Task DeleteComment(int id); 

        void ValidateComment(Comment comment);

        Task<List<Comment>> GetAllCommentsForArticle(Article article);

    }
}
