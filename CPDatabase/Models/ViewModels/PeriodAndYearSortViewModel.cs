namespace CPDatabase.Models
{
    public class PeriodAndYearSortViewModel
    {
        public NTPeriodAndYearSortState NameSort { get; private set; }
        public NTPeriodAndYearSortState GiggiSort { get; private set; }
        public NTPeriodAndYearSortState JbouSort { get; private set; }
        public NTPeriodAndYearSortState ValSort { get; private set; }
        public NTPeriodAndYearSortState Current { get; private set; }

        public PeriodAndYearSortViewModel(NTPeriodAndYearSortState sortOrder)
        {
            NameSort = sortOrder == NTPeriodAndYearSortState.NameAsc ? NTPeriodAndYearSortState.NameDesc : NTPeriodAndYearSortState.NameAsc;
            GiggiSort = sortOrder == NTPeriodAndYearSortState.GiggiAsc ? NTPeriodAndYearSortState.GiggiDesc : NTPeriodAndYearSortState.GiggiAsc;
            JbouSort = sortOrder == NTPeriodAndYearSortState.JbouAsc ? NTPeriodAndYearSortState.JbouDesc : NTPeriodAndYearSortState.JbouAsc;
            ValSort = sortOrder == NTPeriodAndYearSortState.ValAsc ? NTPeriodAndYearSortState.ValDesc : NTPeriodAndYearSortState.ValAsc;
            Current = sortOrder;
        }
    }
}