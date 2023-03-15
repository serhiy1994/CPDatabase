using System.Linq;
using CPDatabase.Models;

namespace CPDatabase
{
    public static class SampleData
    {
        public static void Initialize(CPDBContext cpdbcontext)
        {
            if (!cpdbcontext.CountryClub.Any())
            {
                cpdbcontext.CountryClub.AddRange(
                    new CountryClub { Id = 1, CountryClubName = "Contry 1", HasSub = false, Subcountry = null, Giggi = true, Jbou = true, Val = true },
                    new CountryClub { Id = 2, CountryClubName = "Contry 2", HasSub = false, Subcountry = null, Giggi = true, Jbou = true, Val = true },
                    new CountryClub { Id = 3, CountryClubName = "Contry 3", HasSub = false, Subcountry = null, Giggi = true, Jbou = true, Val = true }
                    );
                cpdbcontext.SaveChanges();
            }
        }
    }
}