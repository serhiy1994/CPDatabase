using System.Collections.Generic;

namespace CPDatabase.Models
{
    public partial class Club
    {
        public Club()
        {
            Team = new HashSet<Team>();
        }

        public int Id { get; set; } = default!;
        public string ClubName { get; set; } = default!;
        public string Country { get; set; } = default!;
        public bool Giggi { get; set; } = default!;
        public bool Jbou { get; set; } = default!;
        public bool Val { get; set; } = default!;

        public virtual CountryClub CountryNavigation { get; set; } = default!;
        public virtual ICollection<Team> Team { get; set; } = default!;
    }
}