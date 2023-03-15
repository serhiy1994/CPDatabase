using System.Collections.Generic;

namespace CPDatabase.Models
{
    public partial class LeagueNT
    {
        public LeagueNT()
        {
            NationalTeam = new HashSet<NationalTeam>();
        }

        public int Id { get; set; } = default!;
        public string LeagueNTName { get; set; } = default!;
        public string? Year { get; set; }
        public string? Period { get; set; }
        public bool HasNonQual { get; set; } = default!;
        public bool Giggi { get; set; } = default!;
        public bool Jbou { get; set; } = default!;
        public bool Val { get; set; } = default!;

        public virtual Period PeriodNavigation { get; set; } = default!;
        public virtual Year YearNavigation { get; set; } = default!;
        public virtual ICollection<NationalTeam> NationalTeam { get; set; } = default!;
    }
}