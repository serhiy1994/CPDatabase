namespace CPDatabase.Models
{
    public class Club
    {
        public int Id { get; set; }
        public string ClubName { get; set; }
        public CountryClub Country { get; set; }
        public bool Giggi { get; set; }
        public bool Jbou { get; set; }
        public bool Val { get; set; }
    }
}