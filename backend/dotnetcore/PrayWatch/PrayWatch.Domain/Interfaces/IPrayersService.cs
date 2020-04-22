using PrayWatch.Domain.Models;
using System.Collections.Generic;

namespace PrayWatch.Domain.Interfaces
{
    public interface IPrayersService
    {
        List<PrayerModel> GetAllBy(string purpose);
    }
}
