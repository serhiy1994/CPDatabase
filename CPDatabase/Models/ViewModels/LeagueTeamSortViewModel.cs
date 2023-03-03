using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPDatabase.Models
{
    public class LeagueTeamSortViewModel
    {
        public TeamLeagueSortState NameSort { get; private set; }
        public TeamLeagueSortState HalfDecadeSort { get; private set; }
        public TeamLeagueSortState SeasonSort { get; private set; }
        public TeamLeagueSortState GiggiSort { get; private set; }
        public TeamLeagueSortState JbouSort { get; private set; }
        public TeamLeagueSortState ValSort { get; private set; }
        public TeamLeagueSortState Current { get; private set; }

        public LeagueTeamSortViewModel(TeamLeagueSortState sortOrder)
        {
            NameSort = sortOrder == TeamLeagueSortState.NameAsc ? TeamLeagueSortState.NameDesc : TeamLeagueSortState.NameAsc;
            HalfDecadeSort = sortOrder == TeamLeagueSortState.HalfDecadeAsc ? TeamLeagueSortState.HalfDecadeDesc : TeamLeagueSortState.HalfDecadeAsc;
            SeasonSort = sortOrder == TeamLeagueSortState.SeasonAsc ? TeamLeagueSortState.SeasonDesc : TeamLeagueSortState.SeasonAsc;
            GiggiSort = sortOrder == TeamLeagueSortState.GiggiAsc ? TeamLeagueSortState.GiggiDesc : TeamLeagueSortState.GiggiAsc;
            JbouSort = sortOrder == TeamLeagueSortState.JbouAsc ? TeamLeagueSortState.JbouDesc : TeamLeagueSortState.JbouAsc;
            ValSort = sortOrder == TeamLeagueSortState.ValAsc ? TeamLeagueSortState.ValDesc : TeamLeagueSortState.ValAsc;
            Current = sortOrder;
        }
    }
}
