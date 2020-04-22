using System.Collections.Generic;

namespace PrayWatch.Domain.Models
{
    public class WatchModel
    {
        public int TimeRange { get; set; }
        public List<PrayerModel> Prayers { get; set; }
    }
}
