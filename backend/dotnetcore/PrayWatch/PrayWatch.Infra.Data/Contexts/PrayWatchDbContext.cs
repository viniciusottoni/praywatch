using Microsoft.EntityFrameworkCore;
using PrayWatch.Domain.Entities;

namespace PrayWatch.Infra.Data.Contexts
{
    public class PrayWatchDbContext : DbContext
    {
        public DbSet<CountryEntity> Countries { get; set; }
        public DbSet<PrayerEntity> Prayers { get; set; }
        public DbSet<PurposeEntity> Purposes { get; set; }
        public DbSet<WatchEntity> Watches { get; set; }

        public PrayWatchDbContext(DbContextOptions<PrayWatchDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Soft Delete
            modelBuilder.Entity<CountryEntity>().HasQueryFilter(x => !x.Deleted);
            modelBuilder.Entity<PrayerEntity>().HasQueryFilter(x => !x.Deleted);
            modelBuilder.Entity<PurposeEntity>().HasQueryFilter(x => !x.Deleted);
            modelBuilder.Entity<WatchEntity>().HasQueryFilter(x => !x.Deleted);

            // Purpose - Watch
            modelBuilder.Entity<PurposeWatchLink>()
                .HasKey(x => new { x.PurposeId, x.WatchId });

            modelBuilder.Entity<PurposeWatchLink>()
              .HasOne(x => x.Purpose)
              .WithMany(x => x.PurposeWatchLink)
              .HasForeignKey(x => x.PurposeId);

            modelBuilder.Entity<PurposeWatchLink>()
              .HasOne(x => x.Watch)
              .WithMany(x => x.PurposeWatchLink)
              .HasForeignKey(x => x.WatchId);

            // Watch - Prayer
            modelBuilder.Entity<WatchPrayerLink>()
                .HasKey(x => new { x.WatchId, x.PrayerId });

            modelBuilder.Entity<WatchPrayerLink>()
              .HasOne(x => x.Watch)
              .WithMany(x => x.WatchPrayerLink)
              .HasForeignKey(x => x.WatchId);

            modelBuilder.Entity<WatchPrayerLink>()
              .HasOne(x => x.Prayer)
              .WithMany(x => x.WatchPrayerLink)
              .HasForeignKey(x => x.PrayerId);
        }
    }
}
