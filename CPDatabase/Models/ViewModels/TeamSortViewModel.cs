namespace CPDatabase.Models
{
    public class TeamSortViewModel
    {
        public TeamSortState NameSort { get; private set; }
        public TeamSortState FixedNameSort { get; private set; }
        public TeamSortState ClubSort { get; private set; }
        public TeamSortState LeagueSort { get; private set; }
        public TeamSortState HalfDecadeSort { get; private set; }
        public TeamSortState SeasonSort { get; private set; }
        public TeamSortState GiggiSort { get; private set; }
        public TeamSortState JbouSort { get; private set; }
        public TeamSortState ValSort { get; private set; }
        public TeamSortState Current { get; private set; }

        public TeamSortViewModel(TeamSortState sortOrder)
        {
            NameSort = sortOrder == TeamSortState.NameAsc ? TeamSortState.NameDesc : TeamSortState.NameAsc;
            FixedNameSort = sortOrder == TeamSortState.FixedNameAsc ? TeamSortState.FixedNameDesc : TeamSortState.FixedNameAsc;
            ClubSort = sortOrder == TeamSortState.ClubAsc ? TeamSortState.ClubDesc : TeamSortState.ClubAsc;
            LeagueSort = sortOrder == TeamSortState.LeagueAsc ? TeamSortState.LeagueDesc : TeamSortState.LeagueAsc;
            HalfDecadeSort = sortOrder == TeamSortState.HalfDecadeAsc ? TeamSortState.HalfDecadeDesc : TeamSortState.HalfDecadeAsc;
            SeasonSort = sortOrder == TeamSortState.SeasonAsc ? TeamSortState.SeasonDesc : TeamSortState.SeasonAsc;
            GiggiSort = sortOrder == TeamSortState.GiggiAsc ? TeamSortState.GiggiDesc : TeamSortState.GiggiAsc;
            JbouSort = sortOrder == TeamSortState.JbouAsc ? TeamSortState.JbouDesc : TeamSortState.JbouAsc;
            ValSort = sortOrder == TeamSortState.ValAsc ? TeamSortState.ValDesc : TeamSortState.ValAsc;
            Current = sortOrder;
        }
    }
}