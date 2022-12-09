namespace CPDatabase.Models
{
    public class LeagueTeam
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public HalfDecade? HalfDecade { get; set; }
        public Season? Season { get; set; }
        public bool Giggi { get; set; }
        public bool Jbou { get; set; }
        public bool Val { get; set; }
    }
}