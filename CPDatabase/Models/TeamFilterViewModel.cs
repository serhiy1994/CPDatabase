using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CPDatabase.Models
{
    public class TeamFilterViewModel
    {
        public SelectList Seasons { get; private set; }
        public int? SelectedSeason { get; private set; }
        public SelectList HalfDecades { get; private set; }
        public int? SelectedHalfDecade { get; private set; }
        public TeamFilterViewModel(List<Season> seasons, List<HalfDecade> halfDecades, int? season, int? halfDecade)
        {
            seasons.Insert(0, new Season { SeasonName = "All", Id = -1 });
            seasons.Insert(1, new Season { SeasonName = "Not set", Id = 0 });
            halfDecades.Insert(0, new HalfDecade { HalfDecadeName = "All", Id = -1 });
            halfDecades.Insert(1, new HalfDecade { HalfDecadeName = "Not set", Id = 0 });
            Seasons = new SelectList(seasons, "Id", "SeasonName", season);
            HalfDecades = new SelectList(halfDecades, "Id", "HalfDecadeName", halfDecade);
            SelectedSeason = season;
            SelectedHalfDecade = halfDecade;
        }
    }
}