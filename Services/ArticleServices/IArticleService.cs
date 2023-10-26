using BrowseClimate.Models;

namespace BrowseClimate.Services.ArticleServices
{
    public interface IArticleService 
    {
        Task<int> CreateArticle(Article article);

        Task<Article> GetArticle(int id);

        Task UpdateArticle(Article article);
        Task DeleteArticle(int id);

        Task RateArticle(User user, int note);

        Task<List<Article>> GetAllArticles();

        void ValidateArticle(Article article);


        Task<List<Comment>> GetArticleComments(int articleId);

        Task<List<Article>> GetUserArticles(int id);

        Task<List<Article>> GetDiscoverArticles(int userId);

        Task <List<Article>> GetTopArticles();

        Task <List<User>> GetLikesOnArticle(int articleId);

        Task AddLike(int articleId, int userId);

        Task RemoveLike(int articleId, int userId);


    }
}
