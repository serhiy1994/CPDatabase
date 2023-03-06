using System.Collections.Generic;

namespace CPDatabase.Models
{
    public class NTsYearViewModel
    {
        public IEnumerable<Year> Years { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public PeriodAndYearSortViewModel SortViewModel { get; set; }
    }
}