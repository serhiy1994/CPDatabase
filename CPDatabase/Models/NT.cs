namespace CPDatabase.Models
{
    public class NT
    {
        public int Id { get; set; }
        public string NTName { get; set; }
        public CountryNT Country { get; set; }
        public LeagueNT LeagueNT { get; set; }
        public Year? Year { get; set; }
        public Period? Period { get; set; }
        public bool NotQual { get; set; }
        public bool Giggi { get; set; }
        public bool Jbou { get; set; }
        public bool Val { get; set; }
    }
}