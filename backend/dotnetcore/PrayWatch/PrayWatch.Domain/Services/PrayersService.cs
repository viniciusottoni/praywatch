using System.Collections.Generic;
using PrayWatch.Domain.Interfaces;
using PrayWatch.Domain.Models;

namespace PrayWatch.Domain.Services
{
    public class PrayersService : IPrayersService
    {
        private readonly IPrayersRepository _prayersRepository;

        public PrayersService(IPrayersRepository prayersRepository)
        {
            _prayersRepository = prayersRepository;
        }

        public List<PrayerModel> GetAllBy(string purpose)
        {
            var entities = _prayersRepository.GetAllBy(purpose);

            if (entities is null)
            {
                return new List<PrayerModel>();
            }

            return entities.ConvertAll(x => new PrayerModel()
            {
                Id = x.Id,
                Name = x.FullName,
                Country = x.Country.IsoCode
            });
        }
    }
}
