namespace BrowseClimate.Models
{
    public class Fact
    {
        public int Id { get; set; }

        public string Title { get; set; }   

        public string Description { get; set; } 

        public DateTime CreatedAt { get; set; }

        public int CityId { get; set; }

        public int Likes { get; set; }

        public int Timestamp { get; set; }


    }
}
