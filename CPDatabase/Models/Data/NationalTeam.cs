namespace CPDatabase.Models
{
    public partial class NationalTeam
    {
        public int Id { get; set; } = default!;
        public string NTName { get; set; } = default!;
        public string Country { get; set; } = default!;
        public string LeagueNT { get; set; } = default!;
        public string? Year { get; set; }
        public string? Period { get; set; }
        public bool NotQual { get; set; } = default!;
        public bool Giggi { get; set; } = default!;
        public bool Jbou { get; set; } = default!;
        public bool Val { get; set; } = default!;

        public virtual CountryNT CountryNavigation { get; set; } = default!;
        public virtual LeagueNT LeagueNtNavigation { get; set; } = default!;
        public virtual Period PeriodNavigation { get; set; } = default!;
        public virtual Year YearNavigation { get; set; } = default!;
    }
}