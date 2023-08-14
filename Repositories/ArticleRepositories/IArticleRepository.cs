using BrowseClimate.Models;

namespace BrowseClimate.Repositories.ArticleRepositories
{
    public interface IArticleRepository
    {
        void CreateArticle(Article article);

        Article GetArticle(int id);
        
        void UpdateArticle (Article article);   

        void DeleteArticle(int id); 


    }
}
