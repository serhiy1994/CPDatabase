namespace CPDatabase.Models
{
    public class LeagueNT
    {
        public int Id { get; set; }
        public string LeagueNTName { get; set; }
        public Year? Year { get; set; }
        public Period? Period { get; set; }
        public bool HasNonQual { get; set; } 
        public bool Giggi { get; set; }
        public bool Jbou { get; set; }
        public bool Val { get; set; }
    }
}