using PrayWatch.Domain.Entities;
using System.Collections.Generic;

namespace PrayWatch.Domain.Interfaces
{
    public interface IPrayersRepository
    {
        List<PrayerEntity> GetAllBy(string purpose);
    }
}
