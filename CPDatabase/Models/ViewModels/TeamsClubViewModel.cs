using System.Collections.Generic;

namespace CPDatabase.Models
{
    public class TeamsClubViewModel
    {
        public IEnumerable<Club> Clubs { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public ClubSortViewModel SortViewModel { get; set; }
    }
}