using System.Collections.Generic;

namespace CPDatabase.Models
{
    public class TeamsSeasonViewModel
    {
        public IEnumerable<Season> Seasons { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public HalfDecadeAndSeasonSortViewModel SortViewModel { get; set; }
    }
}