using System.Collections.Generic;

namespace CPDatabase.Models
{
    public class TeamsHalfDecadeViewModel
    {
        public IEnumerable<HalfDecade> HalfDecades { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public HalfDecadeAndSeasonSortViewModel SortViewModel { get; set; }
    }
}