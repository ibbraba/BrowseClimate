using BrowseClimate.Models;
using BrowseClimate.Repositories.ArticleRepositories;
using Microsoft.Identity.Client;

namespace BrowseClimate.Services.ArticleServices
{
    public class ArticleService : IArticleService
    {
        private IArticleRepository _articleRepository;

        public ArticleService() {

             _articleRepository = new ArticleRepository();   

        }


        public ArticleService(IArticleRepository articleRepository) { 
        
                  _articleRepository = articleRepository;
        }


        public async Task CreateArticle(Article article)
        {

            ValidateArticle(article);
            article.CreatedAt = DateTime.Now;
            //TODO: Article CreatedBy & isAdminArticle
            article.Likes = 0; 
            article.Views = 0;
           

            await _articleRepository.CreateArticle(article);


            
        }

        public async Task DeleteArticle(Article article)
        {


            await _articleRepository.DeleteArticle(article.Id); 
        }

        public async Task<List<Article>> GetAllArticles()
        {
            if(_articleRepository == null)
            {
                _articleRepository = new ArticleRepository();
            }

            List<Article> articles = await _articleRepository.GetAllArticles();
            return articles;
        }

        public async Task<Article> GetArticle(int id)
        {
            Article article = await _articleRepository.GetArticle(id);
            return article;
        }

        public async Task RateArticle(User user, int note)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateArticle(Article article)
        {
            ValidateArticle(article);
            article.UpdatedAt = DateTime.Now;
            
            await _articleRepository.UpdateArticle(article);
        }

        public void ValidateArticle(Article article)
        {
            if (String.IsNullOrEmpty(article.Title) || String.IsNullOrEmpty(article.Content) || String.IsNullOrEmpty(article.Description)) 
            {
                 throw new ArgumentException("Veuillez indiquer le titre, la description et le contenu de votre article");            
            }

        }


        //ADD LIKE 


        //ADD RATE
    }
}
