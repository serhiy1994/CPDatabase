using Microsoft.EntityFrameworkCore;

namespace CPDatabase.Models
{
    public partial class CPDBContext : DbContext
    {
        public virtual DbSet<Club> Club { get; set; } = default!;
        public virtual DbSet<CountryClub> CountryClub { get; set; } = default!;
        public virtual DbSet<CountryNT> CountryNT { get; set; } = default!;
        public virtual DbSet<FeedbackLog> FeedbackLog { get; set; } = default!;
        public virtual DbSet<HalfDecade> HalfDecade { get; set; } = default!;
        public virtual DbSet<LeagueNT> LeagueNT { get; set; } = default!;
        public virtual DbSet<LeagueTeam> LeagueTeam { get; set; } = default!;
        public virtual DbSet<NationalTeam> NationalTeam { get; set; } = default!;
        public virtual DbSet<Period> Period { get; set; } = default!;
        public virtual DbSet<Season> Season { get; set; } = default!;
        public virtual DbSet<Team> Team { get; set; } = default!;
        public virtual DbSet<User> User { get; set; } = default!;
        public virtual DbSet<Year> Year { get; set; } = default!;

        public CPDBContext(DbContextOptions<CPDBContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Club>(entity =>
            {
                entity.HasIndex(e => e.ClubName).HasDatabaseName("IX_Club").IsUnique();
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.ClubName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Country).IsRequired().HasMaxLength(50);
                entity.HasOne(d => d.CountryNavigation).WithMany(p => p.Club).HasPrincipalKey(p => p.CountryClubName).HasForeignKey(d => d.Country).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Club__Country__123EB7A3");
            });

            modelBuilder.Entity<CountryClub>(entity =>
            {
                entity.HasIndex(e => e.CountryClubName).HasDatabaseName("IX_CountryTeam").IsUnique();                
                entity.Property(e => e.CountryClubName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Subcountry).HasMaxLength(50);
            });

            modelBuilder.Entity<CountryNT>(entity =>
            {
                entity.ToTable("CountryNT");
                entity.HasIndex(e => e.CountryNTName).HasDatabaseName("IX_CountryNT").IsUnique();
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.CountryNTName).IsRequired().HasColumnName("CountryNTName").HasMaxLength(50);
                entity.Property(e => e.Subcountry).HasMaxLength(50);
            });

            modelBuilder.Entity<FeedbackLog>(entity =>
            {
                entity.HasKey(e => e.MessageId);
                entity.Property(e => e.DateMessage).HasColumnType("smalldatetime");
                entity.Property(e => e.DateReply).HasColumnType("smalldatetime");
                entity.Property(e => e.Email).IsRequired().HasMaxLength(50);                
                entity.Property(e => e.Message).IsRequired().HasMaxLength(1000);
                entity.Property(e => e.Reply).HasMaxLength(1000);
                entity.Property(e => e.Username).IsRequired().HasMaxLength(20);
            });

            modelBuilder.Entity<HalfDecade>(entity =>
            {
                entity.HasIndex(e => e.HalfDecadeName).HasDatabaseName("IX_Half-decade").IsUnique();
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.HalfDecadeName).IsRequired().HasMaxLength(10);
            });

            modelBuilder.Entity<LeagueNT>(entity =>
            {
                entity.ToTable("LeagueNT");
                entity.HasIndex(e => e.LeagueNTName).HasDatabaseName("IX_LeagueNT").IsUnique();
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.LeagueNTName).IsRequired().HasColumnName("LeagueNTName").HasMaxLength(50);
                entity.Property(e => e.Period).HasMaxLength(10);
                entity.Property(e => e.Year).HasMaxLength(4);
                entity.HasOne(d => d.PeriodNavigation).WithMany(p => p.LeagueNT).HasPrincipalKey(p => p.PeriodName).HasForeignKey(d => d.Period).HasConstraintName("FK__LeagueNT__Period__0E6E26BF");
                entity.HasOne(d => d.YearNavigation).WithMany(p => p.LeagueNt).HasPrincipalKey(p => p.YearName).HasForeignKey(d => d.Year).HasConstraintName("FK__LeagueNT__Year__114A936A");
            });

            modelBuilder.Entity<LeagueTeam>(entity =>
            {
                entity.HasIndex(e => e.Name).HasDatabaseName("IX_LeagueTeam").IsUnique();
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.HalfDecade).HasMaxLength(10);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Season).HasMaxLength(10);
                entity.HasOne(d => d.HalfDecadeNavigation).WithMany(p => p.LeagueTeam).HasPrincipalKey(p => p.HalfDecadeName).HasForeignKey(d => d.HalfDecade).HasConstraintName("FK__LeagueTea__HalfD__0B91BA14");                
                entity.HasOne(d => d.SeasonNavigation).WithMany(p => p.LeagueTeam).HasPrincipalKey(p => p.SeasonName).HasForeignKey(d => d.Season).HasConstraintName("FK__LeagueTea__Seaso__0A9D95DB");
            });

            modelBuilder.Entity<NationalTeam>(entity =>
            {
                entity.HasIndex(e => e.NTName).HasDatabaseName("IX_NationalTeam").IsUnique();
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Country).IsRequired().HasMaxLength(50);
                entity.Property(e => e.LeagueNT).IsRequired().HasColumnName("LeagueNT").HasMaxLength(50);                
                entity.Property(e => e.NTName).IsRequired().HasColumnName("NTName").HasMaxLength(50);
                entity.Property(e => e.Period).HasMaxLength(10);
                entity.Property(e => e.Year).HasMaxLength(4);
                entity.HasOne(d => d.CountryNavigation).WithMany(p => p.NationalTeam).HasPrincipalKey(p => p.CountryNTName).HasForeignKey(d => d.Country).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__NationalT__Count__160F4887");
                entity.HasOne(d => d.LeagueNtNavigation).WithMany(p => p.NationalTeam).HasPrincipalKey(p => p.LeagueNTName).HasForeignKey(d => d.LeagueNT).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__NationalT__Leagu__17F790F9");
                entity.HasOne(d => d.PeriodNavigation).WithMany(p => p.NationalTeam).HasPrincipalKey(p => p.PeriodName).HasForeignKey(d => d.Period).HasConstraintName("FK__NationalT__Perio__19DFD96B");
                entity.HasOne(d => d.YearNavigation).WithMany(p => p.NationalTeam).HasPrincipalKey(p => p.YearName).HasForeignKey(d => d.Year).HasConstraintName("FK__NationalTe__Year__18EBB532");
            });

            modelBuilder.Entity<Period>(entity =>
            {
                entity.HasIndex(e => e.PeriodName).HasDatabaseName("IX_Period").IsUnique();
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.PeriodName).IsRequired().HasMaxLength(10);
            });

            modelBuilder.Entity<Season>(entity =>
            {
                entity.HasIndex(e => e.SeasonName).HasDatabaseName("IX_Season").IsUnique();
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.SeasonName).IsRequired().HasMaxLength(10);
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.HasIndex(e => e.FixedTeamName).HasDatabaseName("IX_Team_1").IsUnique();
                entity.HasIndex(e => e.TeamName).HasDatabaseName("IX_Team").IsUnique();
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Club).IsRequired().HasMaxLength(50);
                entity.Property(e => e.FixedTeamName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.HalfDecade).HasMaxLength(10);
                entity.Property(e => e.LeagueTeam).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Season).HasMaxLength(10);
                entity.Property(e => e.TeamName).IsRequired().HasMaxLength(50);
                entity.HasOne(d => d.ClubNavigation).WithMany(p => p.Team).HasPrincipalKey(p => p.ClubName).HasForeignKey(d => d.Club).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_Team_Club");
                entity.HasOne(d => d.HalfDecadeNavigation).WithMany(p => p.Team).HasPrincipalKey(p => p.HalfDecadeName).HasForeignKey(d => d.HalfDecade).HasConstraintName("FK_Team_HalfDecade");
                entity.HasOne(d => d.LeagueTeamNavigation).WithMany(p => p.Team).HasPrincipalKey(p => p.Name).HasForeignKey(d => d.LeagueTeam).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_Team_LeagueTeam");
                entity.HasOne(d => d.SeasonNavigation).WithMany(p => p.Team).HasPrincipalKey(p => p.SeasonName).HasForeignKey(d => d.Season).HasConstraintName("FK_Team_Season");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_User").IsUnique();
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Password).IsRequired().HasMaxLength(10);
                entity.Property(e => e.Username).IsRequired().HasMaxLength(25);
            });

            modelBuilder.Entity<Year>(entity =>
            {
                entity.HasIndex(e => e.YearName).HasDatabaseName("IX_Year").IsUnique();
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.YearName).IsRequired().HasMaxLength(4);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}