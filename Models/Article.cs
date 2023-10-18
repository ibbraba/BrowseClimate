namespace BrowseClimate.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public int CreatedBy { get; set; }

        public bool isAdminArticle { get; set; }

        public int Likes { get; set; }

        public int Views { get; set; }

        public List<Comment> Comments = new List<Comment>();

        public string ImageURL { get; set; }
    }
}
