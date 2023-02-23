using System.Collections.Generic;

namespace CPDatabase.Models
{
    public class TeamsAllViewModel
    {
        public IEnumerable<Team> Teams { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public TeamFilterViewModel FilterViewModel { get; set; }
        public TeamSortViewModel SortViewModel { get; set; }
    }
}