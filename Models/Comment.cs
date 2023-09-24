namespace BrowseClimate.Models
{
    public class Comment
    {
        public int Id { get; set; } 
        public int ArticleId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        
        public DateTime UpdatedAt { get; set; }
        public int CreatedBy { get; set; } 
    }
}
