namespace CPDatabase.Models
{
    public class CountryClub
    {
        public int Id { get; set; }
        public string CountryClubName { get; set; }
        public bool HasSub { get; set; }
        public string? Subcountry { get; set; }
        public bool Giggi { get; set; }
        public bool Jbou { get; set; }
        public bool Val { get; set; }
    }
}