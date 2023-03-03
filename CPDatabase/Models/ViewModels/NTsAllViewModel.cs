using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
