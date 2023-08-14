namespace BrowseClimate.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }    
        public string FirstName{ get; set; }
        public string Email { get; set; }
        public string Pseudo { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public DateTime CreatedAt { get; set; }
        public City FavoriteCity { get; set; }


        public override string ToString()
        {
            if (Pseudo != null)
                return Pseudo;

            else return "Anonymous"; 
        }
    }
}
