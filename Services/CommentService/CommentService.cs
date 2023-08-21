using BrowseClimate.Models;
using BrowseClimate.Repositories.CommentRepositories;

namespace BrowseClimate.Services.CommentService
{
    public class CommentService : ICommentService
    {
        private ICommentRepository _commentRepository;

        public CommentService()
        {
            _commentRepository = new CommentRepository();
        }

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task CreateComment(Comment comment)
        {
            ValidateComment(comment); 
            //SET USER 
            //SET Article 
            comment.CreatedAt = DateTime.Now;
            await _commentRepository.CreateComment(comment);
        }

        public async Task DeleteComment(int id)
        {
           await _commentRepository.DeleteComment(id);
        }

        public async Task<List<Comment>> GetAllCommentsForArticle(Article article)
        {
            List<Comment> comments = await _commentRepository.GetAllCommentsForArticle(article.Id);
            return comments;

        }

        public async Task<Comment> GetComment(int id)
        {
            Comment comment = await _commentRepository.GetComment(id);
            return comment; 

        }

        public async Task UpdateComment(Comment comment)
        {
            ValidateComment(comment); 
            comment.UpdatedAt = DateTime.Now;
            await _commentRepository.UpdateComment(comment);
        }

        public void ValidateComment(Comment comment)
        {
            if(String.IsNullOrEmpty(comment.Content) )
            {
                throw new ArgumentOutOfRangeException("La longueur du commentaire est invalide");
            } 

            if(comment.Content.Length > 800)
            {
                throw new ArgumentOutOfRangeException("Le commentaire ne doit pas dépaasser 800 caractères");
            }



        }
    }
}
