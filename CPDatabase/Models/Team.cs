namespace CPDatabase.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string TeamName { get; set; }
        public string FixedTeamName { get; set; }
        public Club Club { get; set; }
        public HalfDecade? HalfDecade { get; set; }
        public Season? Season { get; set; }
        public bool Giggi { get; set; }
        public bool Jbou { get; set; }
        public bool Val { get; set; }
    }
}