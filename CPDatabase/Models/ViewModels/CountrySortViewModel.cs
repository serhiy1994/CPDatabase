using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPDatabase.Models
{
    public class CountrySortViewModel
    {
        public TeamCountrySortState NameSort { get; private set; }
        public TeamCountrySortState HasSubSort { get; private set; }
        public TeamCountrySortState SubcountrySort { get; private set; }
        public TeamCountrySortState GiggiSort { get; private set; }
        public TeamCountrySortState JbouSort { get; private set; }
        public TeamCountrySortState ValSort { get; private set; }
        public TeamCountrySortState Current { get; private set; }

        public CountrySortViewModel(TeamCountrySortState sortOrder)
        {
            NameSort = sortOrder == TeamCountrySortState.NameAsc ? TeamCountrySortState.NameDesc : TeamCountrySortState.NameAsc;
            HasSubSort = sortOrder == TeamCountrySortState.HasSubAsc ? TeamCountrySortState.HasSubDesc : TeamCountrySortState.HasSubAsc;
            SubcountrySort = sortOrder == TeamCountrySortState.SubcountryAsc ? TeamCountrySortState.SubcountryDesc : TeamCountrySortState.SubcountryAsc;
            GiggiSort = sortOrder == TeamCountrySortState.GiggiAsc ? TeamCountrySortState.GiggiDesc : TeamCountrySortState.GiggiAsc;
            JbouSort = sortOrder == TeamCountrySortState.JbouAsc ? TeamCountrySortState.JbouDesc : TeamCountrySortState.JbouAsc;
            ValSort = sortOrder == TeamCountrySortState.ValAsc ? TeamCountrySortState.ValDesc : TeamCountrySortState.ValAsc;
            Current = sortOrder;
        }
    }
}
