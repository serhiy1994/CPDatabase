using System;
using System.Collections.Generic;

namespace CPDatabase.Models
{
    public partial class LeagueTeam
    {
        public LeagueTeam()
        {
            Team = new HashSet<Team>();
        }

        public int Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string? HalfDecade { get; set; }
        public string? Season { get; set; }
        public bool Giggi { get; set; } = default!;
        public bool Jbou { get; set; } = default!;
        public bool Val { get; set; } = default!;

        public virtual HalfDecade HalfDecadeNavigation { get; set; } = default!;
        public virtual Season SeasonNavigation { get; set; } = default!;
        public virtual ICollection<Team> Team { get; set; } = default!;
    }
}