using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPDatabase.Models
{
    public class NTFilterViewModel
    {
        public SelectList Years { get; private set; }
        public int? SelectedYear { get; private set; }
        public SelectList Periods { get; private set; }
        public int? SelectedPeriod { get; private set; }
        public NTFilterViewModel(List<Year> years, List<Period> periods, int? year, int? period)
        {
            years.Insert(0, new Year { YearName = "All", Id = -1 });
            years.Insert(1, new Year { YearName = "Not set", Id = 0 });
            periods.Insert(0, new Period { PeriodName = "All", Id = -1 });
            periods.Insert(1, new Period { PeriodName = "Not set", Id = 0 });
            Years = new SelectList(years, "Id", "YearName", year);
            Periods = new SelectList(periods, "Id", "PeriodName", period);
            SelectedYear = year;
            SelectedPeriod = period;
        }
    }
}
