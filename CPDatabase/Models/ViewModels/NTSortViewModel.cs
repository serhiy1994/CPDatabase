namespace CPDatabase.Models
{
    public class NTSortViewModel
    {
        public NTSortState NameSort { get; private set; }
        public NTSortState CountrySort { get; private set; }
        public NTSortState LeagueSort { get; private set; }
        public NTSortState YearSort { get; private set; }
        public NTSortState PeriodSort { get; private set; }
        public NTSortState NotQualSort { get; private set; }
        public NTSortState GiggiSort { get; private set; }
        public NTSortState JbouSort { get; private set; }
        public NTSortState ValSort { get; private set; }
        public NTSortState Current { get; private set; }

        public NTSortViewModel(NTSortState sortOrder)
        {
            NameSort = sortOrder == NTSortState.NameAsc ? NTSortState.NameDesc : NTSortState.NameAsc;
            CountrySort = sortOrder == NTSortState.CountryAsc ? NTSortState.CountryDesc : NTSortState.CountryAsc;
            LeagueSort = sortOrder == NTSortState.LeagueAsc ? NTSortState.LeagueDesc : NTSortState.LeagueAsc;
            YearSort = sortOrder == NTSortState.YearAsc ? NTSortState.YearDesc : NTSortState.YearAsc;
            PeriodSort = sortOrder == NTSortState.PeriodAsc ? NTSortState.PeriodDesc : NTSortState.PeriodAsc;
            NotQualSort = sortOrder == NTSortState.NotQualAsc ? NTSortState.NotQualDesc : NTSortState.NotQualAsc;
            GiggiSort = sortOrder == NTSortState.GiggiAsc ? NTSortState.GiggiDesc : NTSortState.GiggiAsc;
            JbouSort = sortOrder == NTSortState.JbouAsc ? NTSortState.JbouDesc : NTSortState.JbouAsc;
            ValSort = sortOrder == NTSortState.ValAsc ? NTSortState.ValDesc : NTSortState.ValAsc;
            Current = sortOrder;
        }
    }
}