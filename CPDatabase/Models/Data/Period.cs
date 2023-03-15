using System.Collections.Generic;

namespace CPDatabase.Models
{
    public partial class Period
    {
        public Period()
        {
            LeagueNT = new HashSet<LeagueNT>();
            NationalTeam = new HashSet<NationalTeam>();
        }

        public int Id { get; set; } = default!;
        public string PeriodName { get; set; } = default!;
        public bool Giggi { get; set; } = default!;
        public bool Jbou { get; set; } = default!;
        public bool Val { get; set; } = default!;

        public virtual ICollection<LeagueNT> LeagueNT { get; set; } = default!;
        public virtual ICollection<NationalTeam> NationalTeam { get; set; } = default!;
    }
}