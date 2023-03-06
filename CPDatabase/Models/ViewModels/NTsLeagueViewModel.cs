using System.Collections.Generic;

namespace CPDatabase.Models
{
    public class NTsLeagueViewModel
    {
        public IEnumerable<LeagueNT> LeaguesNT { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public LeagueNTSortViewModel SortViewModel { get; set; }
    }
}