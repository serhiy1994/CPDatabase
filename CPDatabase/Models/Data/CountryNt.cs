using System.Collections.Generic;

namespace CPDatabase.Models
{
    public partial class CountryNT
    {
        public CountryNT()
        {
            NationalTeam = new HashSet<NationalTeam>();
        }

        public int Id { get; set; } = default!;
        public string CountryNTName { get; set; } = default!;
        public bool HasSub { get; set; } = default!;
        public string? Subcountry { get; set; }
        public bool Giggi { get; set; } = default!;
        public bool Jbou { get; set; } = default!;
        public bool Val { get; set; } = default!;

        public virtual ICollection<NationalTeam> NationalTeam { get; set; } = default!;
    }
}