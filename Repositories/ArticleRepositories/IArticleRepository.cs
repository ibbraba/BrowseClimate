using BrowseClimate.Models;

namespace BrowseClimate.Repositories.ArticleRepositories
{
    public interface IArticleRepository
    {
        Task<int> CreateArticle(Article article);

        Task<Article> GetArticle(int id);

        Task<List<Article>> GetAllArticles();
        
        Task UpdateArticle (Article article);   

        Task DeleteArticle(int id);

        Task<List<Article>> GetUserArticles(int id);


        Task<List<Article>> GetTopArticles();

        Task<List<User>> GetLikesOnArticle(int articleId);

        Task<List<Article>> GetArticlesLikedByUser(int userId);



    }
}
