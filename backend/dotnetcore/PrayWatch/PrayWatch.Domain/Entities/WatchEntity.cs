using System.Collections.Generic;

namespace PrayWatch.Domain.Entities
{
    public class WatchEntity : BaseEntity
    {
        public int Id { get; set; }
        public int TimeRange { get; set; }

        public ICollection<PurposeWatchLink> PurposeWatchLink { get; set; }
        public ICollection<WatchPrayerLink> WatchPrayerLink { get; set; }
    }
}
