using System.Collections.Generic;

namespace CPDatabase.Models
{
    public class NTsCountryViewModel
    {
        public IEnumerable<CountryNT> CountriesNT { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public CountrySortViewModel SortViewModel { get; set; }
    }
}