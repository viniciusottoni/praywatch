
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PrayWatch.Domain.Entities;
using PrayWatch.Infra.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PrayWatch.Infra.Data.Repositories.InMemory
{
    public static class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (PrayWatchDbContext context = new PrayWatchDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<PrayWatchDbContext>>()))
            {
                Initialize(context);
            }
        }

        public static void Initialize(PrayWatchDbContext context)
        {
            if (!context.Countries.Any())
            {
                context.Countries.AddRange(
                    new CountryEntity()
                    {
                        Id = 1,
                        IsoCode = "BRA",
                        CreatedDate = DateTime.Now
                    },
                    new CountryEntity()
                    {
                        Id = 2,
                        IsoCode = "USA",
                        CreatedDate = DateTime.Now
                    }
                );
            }

            if (!context.Purposes.Any())
            {
                context.Purposes.AddRange(
                    new PurposeEntity()
                    {
                        Id = 1,
                        Name = "Covid19",
                        CreatedDate = DateTime.Now
                    },
                    new PurposeEntity()
                    {
                        Id = 2,
                        Name = "Ebola",
                        CreatedDate = DateTime.Now,
                        Deleted = true
                    }
                );
            }

            if (!context.Watches.Any())
            {
                context.Watches.AddRange(
                    new WatchEntity()
                    {
                        Id = 1,
                        TimeRange = 0,
                        CreatedDate = DateTime.Now,
                        PurposeWatchLink = new List<PurposeWatchLink>()
                        {
                                new PurposeWatchLink() { PurposeId = 1, WatchId = 1 }
                        }
                    },
                    new WatchEntity()
                    {
                        Id = 2,
                        TimeRange = 5,
                        CreatedDate = DateTime.Now,
                        PurposeWatchLink = new List<PurposeWatchLink>()
                        {
                                new PurposeWatchLink() { PurposeId = 1, WatchId = 2 }
                        }
                    },
                    new WatchEntity()
                    {
                        Id = 3,
                        TimeRange = 10,
                        CreatedDate = DateTime.Now,
                        PurposeWatchLink = new List<PurposeWatchLink>()
                        {
                                new PurposeWatchLink() { PurposeId = 1, WatchId = 3 }
                        }
                    }
                );
            }

            if (!context.Prayers.Any())
            {
                Guid viniciusId = Guid.NewGuid(), raphaelId = Guid.NewGuid();
                context.Prayers.AddRange(
                    new PrayerEntity()
                    {
                        Id = viniciusId,
                        CountryId = 1,
                        CreatedDate = DateTime.Now,
                        FullName = "Vinicius Ottoni",
                        WatchPrayerLink = new List<WatchPrayerLink>()
                        {
                                new WatchPrayerLink() { WatchId = 1, PrayerId = viniciusId },
                                new WatchPrayerLink() { WatchId = 2, PrayerId = viniciusId }
                        }
                    },
                    new PrayerEntity()
                    {
                        Id = raphaelId,
                        CountryId = 2,
                        CreatedDate = DateTime.Now,
                        FullName = "Raphael Ribeiro",
                        WatchPrayerLink = new List<WatchPrayerLink>()
                        {
                                new WatchPrayerLink() { WatchId = 2, PrayerId = raphaelId },
                                new WatchPrayerLink() { WatchId = 3, PrayerId = raphaelId }
                        }
                    }
                );
            }

            context.SaveChanges();
        }
    }
}
