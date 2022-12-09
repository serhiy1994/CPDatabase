using Microsoft.EntityFrameworkCore;

namespace CPDatabase.Models
{
    public class CPDBContext : DbContext
    {
        public DbSet<CountryClub> CountryClub { get; set; }
        public DbSet<CountryNT> CountryNT { get; set; }
        public DbSet<HalfDecade> HalfDecade { get; set; }
        public DbSet<Season> Season { get; set; }
        public DbSet<Year> Year { get; set; }
        public DbSet<Period> Period { get; set; }
        public DbSet<LeagueTeam> LeagueTeam { get; set; }
        public DbSet<LeagueNT> LeagueNT { get; set; }
        public DbSet<Club> Club { get; set; }
        public DbSet<Team> Team { get; set; }
        public DbSet<NT> NT { get; set; }

        public CPDBContext(DbContextOptions<CPDBContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}