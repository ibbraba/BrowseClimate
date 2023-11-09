namespace BrowseClimate.Models
{
    public class City
    {

        public int Id { get; set; }
        public string Name { get; set; }    
        public string Country { get; set; }
        public string Description { get; set; }

        public int NumberResidents { get; set; }
        public DateTime CreatedAt { get; set; }

        public string TimeZone { get; set; }

        public double lat { get; set; }

        public double lon { get; set; }

        public int Temperature { get; set; }

        public string ImageURL { get; set; }

        public List<Fact> Facts { get; set; }  

        public int Timestamp { get; set; } 

        public int Note { get; set; }

        public int NumberFans { get; set; }

    }
}
