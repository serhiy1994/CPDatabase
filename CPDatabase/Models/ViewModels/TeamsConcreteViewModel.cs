using System.Collections.Generic;

namespace CPDatabase.Models
{
    public class TeamsConcreteViewModel
    {
        public IEnumerable<Team> Teams { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public TeamSortViewModel SortViewModel { get; set; }
    }
}