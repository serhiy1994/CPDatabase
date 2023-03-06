using System.Collections.Generic;

namespace CPDatabase.Models
{
    public class NTsPeriodViewModel
    {
        public IEnumerable<Period> Periods { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public PeriodAndYearSortViewModel SortViewModel { get; set; }
    }
}