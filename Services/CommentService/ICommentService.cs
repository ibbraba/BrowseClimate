using BrowseClimate.Models;

namespace BrowseClimate.Services.CommentService
{
    public interface ICommentService
    {
        void CreateComment( Comment comment);

        Comment GetComment(int id);

        void UpdateComment (Comment comment );

        void DeleteComment(int id); 



    }
}
