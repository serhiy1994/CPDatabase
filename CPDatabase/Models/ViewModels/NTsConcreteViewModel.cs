using System.Collections.Generic;

namespace CPDatabase.Models
{
    public class NTsConcreteViewModel
    {
        public IEnumerable<NationalTeam> NTs { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public NTSortViewModel SortViewModel { get; set; }
    }
}