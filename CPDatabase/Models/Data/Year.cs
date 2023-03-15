using System.Collections.Generic;

namespace CPDatabase.Models
{
    public partial class Year
    {
        public Year()
        {
            LeagueNt = new HashSet<LeagueNT>();
            NationalTeam = new HashSet<NationalTeam>();
        }

        public int Id { get; set; } = default!;
        public string YearName { get; set; } = default!;
        public bool Giggi { get; set; } = default!;
        public bool Jbou { get; set; } = default!;
        public bool Val { get; set; } = default!;

        public virtual ICollection<LeagueNT> LeagueNt { get; set; } = default!;
        public virtual ICollection<NationalTeam> NationalTeam { get; set; } = default!;
    }
}