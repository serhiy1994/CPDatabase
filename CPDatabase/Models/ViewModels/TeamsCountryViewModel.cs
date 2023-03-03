using System.Collections.Generic;

namespace CPDatabase.Models
{
    public class TeamsCountryViewModel
    {
        public IEnumerable<CountryClub> Countries { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public CountrySortViewModel SortViewModel { get; set; }
    }
}