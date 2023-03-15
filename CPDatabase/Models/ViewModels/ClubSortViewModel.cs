namespace CPDatabase.Models
{
    public class ClubSortViewModel
    {
        public TeamClubSortState NameSort { get; private set; }
        public TeamClubSortState CountrySort { get; private set; }
        public TeamClubSortState GiggiSort { get; private set; }
        public TeamClubSortState JbouSort { get; private set; }
        public TeamClubSortState ValSort { get; private set; }
        public TeamClubSortState Current { get; private set; }

        public ClubSortViewModel(TeamClubSortState sortOrder)
        {
            NameSort = sortOrder == TeamClubSortState.NameAsc ? TeamClubSortState.NameDesc : TeamClubSortState.NameAsc;
            CountrySort = sortOrder == TeamClubSortState.CountryAsc ? TeamClubSortState.CountryDesc : TeamClubSortState.CountryAsc;
            GiggiSort = sortOrder == TeamClubSortState.GiggiAsc ? TeamClubSortState.GiggiDesc : TeamClubSortState.GiggiAsc;
            JbouSort = sortOrder == TeamClubSortState.JbouAsc ? TeamClubSortState.JbouDesc : TeamClubSortState.JbouAsc;
            ValSort = sortOrder == TeamClubSortState.ValAsc ? TeamClubSortState.ValDesc : TeamClubSortState.ValAsc;
            Current = sortOrder;
        }
    }
}