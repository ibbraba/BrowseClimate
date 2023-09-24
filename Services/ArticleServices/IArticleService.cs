using BrowseClimate.Models;

namespace BrowseClimate.Services.ArticleServices
{
    public interface IArticleService 
    {
        Task CreateArticle(Article article);

        Task<Article> GetArticle(int id);

        Task UpdateArticle(Article article);
        Task DeleteArticle(Article article);

        Task RateArticle(User user, int note);

        Task<List<Article>> GetAllArticles();

        void ValidateArticle(Article article);


        Task<List<Comment>> GetArticleComments(int articleId);


    }
}
