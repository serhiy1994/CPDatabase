namespace CPDatabase.Models
{
    public class HalfDecadeAndSeasonSortViewModel
    {
        public TeamHalfDecadeAndSeasonSortState NameSort { get; private set; }
        public TeamHalfDecadeAndSeasonSortState GiggiSort { get; private set; }
        public TeamHalfDecadeAndSeasonSortState JbouSort { get; private set; }
        public TeamHalfDecadeAndSeasonSortState ValSort { get; private set; }
        public TeamHalfDecadeAndSeasonSortState Current { get; private set; }

        public HalfDecadeAndSeasonSortViewModel(TeamHalfDecadeAndSeasonSortState sortOrder)
        {
            NameSort = sortOrder == TeamHalfDecadeAndSeasonSortState.NameAsc ? TeamHalfDecadeAndSeasonSortState.NameDesc : TeamHalfDecadeAndSeasonSortState.NameAsc;
            GiggiSort = sortOrder == TeamHalfDecadeAndSeasonSortState.GiggiAsc ? TeamHalfDecadeAndSeasonSortState.GiggiDesc : TeamHalfDecadeAndSeasonSortState.GiggiAsc;
            JbouSort = sortOrder == TeamHalfDecadeAndSeasonSortState.JbouAsc ? TeamHalfDecadeAndSeasonSortState.JbouDesc : TeamHalfDecadeAndSeasonSortState.JbouAsc;
            ValSort = sortOrder == TeamHalfDecadeAndSeasonSortState.ValAsc ? TeamHalfDecadeAndSeasonSortState.ValDesc : TeamHalfDecadeAndSeasonSortState.ValAsc;
            Current = sortOrder;
        }
    }
}