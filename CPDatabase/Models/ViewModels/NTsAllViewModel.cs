using System.Collections.Generic;

namespace CPDatabase.Models
{
    public class NTsAllViewModel
    {
        public IEnumerable<NationalTeam> NTs { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public NTFilterViewModel FilterViewModel { get; set; }
        public NTSortViewModel SortViewModel { get; set; }
    }
}