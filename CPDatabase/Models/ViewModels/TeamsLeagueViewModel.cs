using System.Collections.Generic;

namespace CPDatabase.Models
{
    public class TeamsLeagueViewModel
    {
        public IEnumerable<LeagueTeam> LeaguesT { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public LeagueTeamSortViewModel SortViewModel { get; set; }
    }
}