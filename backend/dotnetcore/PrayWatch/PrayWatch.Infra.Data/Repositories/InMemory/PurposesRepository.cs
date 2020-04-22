using Microsoft.EntityFrameworkCore;
using PrayWatch.Domain.Entities;
using PrayWatch.Domain.Interfaces;
using PrayWatch.Infra.Data.Contexts;
using System.Linq;

namespace PrayWatch.Infra.Data.Repositories.InMemory
{
    public class PurposesRepository : IPurposesRepository
    {
        private readonly PrayWatchDbContext _dbContext;

        public PurposesRepository(PrayWatchDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public PurposeEntity GetBy(string purpose)
        {
            var include = $"{nameof(PurposeEntity.PurposeWatchLink)}.{nameof(PurposeWatchLink.Watch)}.{nameof(WatchEntity.WatchPrayerLink)}.{nameof(WatchPrayerLink.Prayer)}.{nameof(PrayerEntity.Country)}";

            return _dbContext.Purposes.Include(include)
                .FirstOrDefault(x => x.Name.ToLower().Equals(purpose.ToLower()));
        }
    }
}
