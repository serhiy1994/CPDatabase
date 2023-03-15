namespace CPDatabase.Models
{
    public class LeagueNTSortViewModel
    {
        public NTLeagueSortState NameSort { get; private set; }
        public NTLeagueSortState PeriodSort { get; private set; }
        public NTLeagueSortState YearSort { get; private set; }
        public NTLeagueSortState GiggiSort { get; private set; }
        public NTLeagueSortState JbouSort { get; private set; }
        public NTLeagueSortState ValSort { get; private set; }
        public NTLeagueSortState Current { get; private set; }

        public LeagueNTSortViewModel(NTLeagueSortState sortOrder)
        {
            NameSort = sortOrder == NTLeagueSortState.NameAsc ? NTLeagueSortState.NameDesc : NTLeagueSortState.NameAsc;
            PeriodSort = sortOrder == NTLeagueSortState.PeriodAsc ? NTLeagueSortState.PeriodDesc : NTLeagueSortState.PeriodAsc;
            YearSort = sortOrder == NTLeagueSortState.YearAsc ? NTLeagueSortState.YearDesc : NTLeagueSortState.YearAsc;
            GiggiSort = sortOrder == NTLeagueSortState.GiggiAsc ? NTLeagueSortState.GiggiDesc : NTLeagueSortState.GiggiAsc;
            JbouSort = sortOrder == NTLeagueSortState.JbouAsc ? NTLeagueSortState.JbouDesc : NTLeagueSortState.JbouAsc;
            ValSort = sortOrder == NTLeagueSortState.ValAsc ? NTLeagueSortState.ValDesc : NTLeagueSortState.ValAsc;
            Current = sortOrder;
        }
    }
}