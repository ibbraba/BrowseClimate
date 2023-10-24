using BrowseClimate.Models;
using BrowseClimate.Repositories.ArticleRepositories;
using BrowseClimate.Repositories.CommentRepositories;
using Microsoft.Identity.Client;

namespace BrowseClimate.Services.ArticleServices
{
    public class ArticleService : IArticleService
    {
        private IArticleRepository _articleRepository;
        private CommentRepository _commentaireRepository;

        public ArticleService() {

             _articleRepository = new ArticleRepository();
            _commentaireRepository = new CommentRepository();

        }


        public ArticleService(IArticleRepository articleRepository) { 
        
                  _articleRepository = articleRepository;
        }


        public async Task<int> CreateArticle(Article article)
        {

            ValidateArticle(article);
            article.CreatedAt = DateTime.Now;
            article.UpdatedAt = DateTime.Now;

            //TODO: Article CreatedBy & isAdminArticle
            article.Likes = 0; 
            article.Views = 0;
           

            int articleId = await _articleRepository.CreateArticle(article);
            return articleId;


            
        }



        public async Task DeleteArticle(int id)
        {
            await _articleRepository.DeleteArticle(id);
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
            article.Timestamp = (int)article.CreatedAt.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            return article;
        }

        public Task<List<Comment>> GetArticleComments(int articleId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Article>> GetDiscoverArticles(int userId)
        {
            
            //User Articles
            List<Article> Userarticle = await _articleRepository.GetUserArticles(userId);
            List<Article> topArticles = await _articleRepository.GetTopArticles();
            List<Article> likedArticles = await _articleRepository.GetArticlesLikedByUser(userId);


            //Articles Liked by user

            List<Article> discoverArticles = new();
            discoverArticles.AddRange(Userarticle);

            foreach(Article article in topArticles)
            {
                 Article topArticle = discoverArticles.Find(x => x.Id == article.Id);

                if (topArticle == null)
                {
                    discoverArticles.Add(article);
                }

            }

            foreach (Article article in likedArticles)
            {
                Article likedArticle = discoverArticles.Find(x => x.Id == article.Id);

                if (likedArticle == null)
                {
                    discoverArticles.Add(article);
                }

            }
     

            discoverArticles = discoverArticles.Distinct().ToList();
            discoverArticles = discoverArticles.OrderBy(x => x.CreatedAt).ToList();
            discoverArticles = discoverArticles.AsEnumerable().Reverse().ToList();



            return discoverArticles;    

            //Top Articles
            //
        }

        public async Task<List<Article>> GetUserArticles(int id)
        {
            List<Article> articles = await _articleRepository.GetUserArticles(id);
            
            return articles;
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


        async Task<List<Article>> IArticleService.GetTopArticles()
        {
            List<Article> articles = await _articleRepository.GetTopArticles();
            return articles;
        }

   

        public async Task<List<User>> GetLikesOnArticle(int articleId)
        {
            List<User> articles = await _articleRepository.GetLikesOnArticle(articleId);
            return articles;

        }

   
     

        //ADD LIKE 


        //ADD RATE
    }
}
