using PrayWatch.Domain.Interfaces;
using PrayWatch.Domain.Models;
using System.Linq;

namespace PrayWatch.Domain.Services
{
    public class PurposesService : IPurposesService
    {
        private readonly IPurposesRepository _purposesRepository;

        public PurposesService(IPurposesRepository purposesRepository)
        {
            _purposesRepository = purposesRepository;
        }

        public PurposeModel GetBy(string purpose)
        {
            var entity = _purposesRepository.GetBy(purpose);

            if (entity is null)
            {
                return null;
            }

            return new PurposeModel()
            {
                Name = entity.Name,
                PrayWatch = entity.PurposeWatchLink?
                    .Select(x => x.Watch).ToList().ConvertAll(watch => new WatchModel()
                    {
                        TimeRange = watch.TimeRange,
                        Prayers = watch.WatchPrayerLink
                            .Select(x => x.Prayer).ToList().ConvertAll(y => new PrayerModel()
                            {
                                Id = y.Id,
                                Name = y.FullName,
                                Country = y.Country.IsoCode
                            })
                    })
            };
        }
    }
}
