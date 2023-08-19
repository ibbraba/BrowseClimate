using BrowseClimate.Models;

namespace BrowseClimate.Services.ArticleServices
{
    public interface IArticleService 
    {
        void CreateArticle(Article article);

        Article GetArticle(int id);

        void UpdateArticle(Article article);
        void DeleteArticle(Article article);

        void RateArticle(User user, int note);

    }
}
