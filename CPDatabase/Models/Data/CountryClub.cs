using System;
using System.Collections.Generic;

namespace CPDatabase.Models
{
    public partial class CountryClub
    {
        public CountryClub()
        {
            Club = new HashSet<Club>();
        }

        public int Id { get; set; } = default!;
        public string CountryClubName { get; set; } = default!;
        public bool HasSub { get; set; } = default!;
        public string? Subcountry { get; set; }
        public bool Giggi { get; set; } = default!;
        public bool Jbou { get; set; } = default!;
        public bool Val { get; set; } = default!;

        public virtual ICollection<Club> Club { get; set; } = default!;
    }
}