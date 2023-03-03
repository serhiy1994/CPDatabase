using System;
using System.Collections.Generic;

namespace CPDatabase.Models
{
    public partial class HalfDecade
    {
        public HalfDecade()
        {
            LeagueTeam = new HashSet<LeagueTeam>();
            Team = new HashSet<Team>();
        }

        public int Id { get; set; } = default!;
        public string HalfDecadeName { get; set; } = default!;
        public bool Giggi { get; set; } = default!;
        public bool Jbou { get; set; } = default!;
        public bool Val { get; set; } = default!;

        public virtual ICollection<LeagueTeam> LeagueTeam { get; set; } = default!;
        public virtual ICollection<Team> Team { get; set; } = default!;
    }
}