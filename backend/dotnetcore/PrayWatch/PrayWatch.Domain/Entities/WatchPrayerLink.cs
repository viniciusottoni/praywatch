using System;

namespace PrayWatch.Domain.Entities
{
    public class WatchPrayerLink
    {
        public int WatchId { get; set; }
        public WatchEntity Watch { get; set; }

        public Guid PrayerId { get; set; }
        public PrayerEntity Prayer { get; set; }
    }
}
