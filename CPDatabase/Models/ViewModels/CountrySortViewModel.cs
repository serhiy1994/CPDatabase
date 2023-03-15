namespace CPDatabase.Models
{
    public class CountrySortViewModel
    {
        public TeamCountryAndNTCountrySortState NameSort { get; private set; }
        public TeamCountryAndNTCountrySortState HasSubSort { get; private set; }
        public TeamCountryAndNTCountrySortState SubcountrySort { get; private set; }
        public TeamCountryAndNTCountrySortState GiggiSort { get; private set; }
        public TeamCountryAndNTCountrySortState JbouSort { get; private set; }
        public TeamCountryAndNTCountrySortState ValSort { get; private set; }
        public TeamCountryAndNTCountrySortState Current { get; private set; }

        public CountrySortViewModel(TeamCountryAndNTCountrySortState sortOrder)
        {
            NameSort = sortOrder == TeamCountryAndNTCountrySortState.NameAsc ? TeamCountryAndNTCountrySortState.NameDesc : TeamCountryAndNTCountrySortState.NameAsc;
            HasSubSort = sortOrder == TeamCountryAndNTCountrySortState.HasSubAsc ? TeamCountryAndNTCountrySortState.HasSubDesc : TeamCountryAndNTCountrySortState.HasSubAsc;
            SubcountrySort = sortOrder == TeamCountryAndNTCountrySortState.SubcountryAsc ? TeamCountryAndNTCountrySortState.SubcountryDesc : TeamCountryAndNTCountrySortState.SubcountryAsc;
            GiggiSort = sortOrder == TeamCountryAndNTCountrySortState.GiggiAsc ? TeamCountryAndNTCountrySortState.GiggiDesc : TeamCountryAndNTCountrySortState.GiggiAsc;
            JbouSort = sortOrder == TeamCountryAndNTCountrySortState.JbouAsc ? TeamCountryAndNTCountrySortState.JbouDesc : TeamCountryAndNTCountrySortState.JbouAsc;
            ValSort = sortOrder == TeamCountryAndNTCountrySortState.ValAsc ? TeamCountryAndNTCountrySortState.ValDesc : TeamCountryAndNTCountrySortState.ValAsc;
            Current = sortOrder;
        }
    }
}