using System;
using System.Collections.Generic;

namespace CPDatabase.Models
{
    public partial class Team
    {
        public int Id { get; set; } = default!;
        public string TeamName { get; set; } = default!;
        public string FixedTeamName { get; set; } = default!;
        public string Club { get; set; } = default!;
        public string LeagueTeam { get; set; } = default!;
        public string? HalfDecade { get; set; }
        public string? Season { get; set; }
        public bool Giggi { get; set; } = default!;
        public bool Jbou { get; set; } = default!;
        public bool Val { get; set; } = default!;

        public virtual Club ClubNavigation { get; set; } = default!;
        public virtual HalfDecade HalfDecadeNavigation { get; set; } = default!;
        public virtual LeagueTeam LeagueTeamNavigation { get; set; } = default!;
        public virtual Season SeasonNavigation { get; set; } = default!;
    }
}