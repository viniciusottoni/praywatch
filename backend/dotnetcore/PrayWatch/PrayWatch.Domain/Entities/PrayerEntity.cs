using System;
using System.Collections.Generic;

namespace PrayWatch.Domain.Entities
{
    public class PrayerEntity : BaseEntity
    {
        public Guid Id { get; set; }
        public int CountryId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }

        public CountryEntity Country { get; set; }
        public ICollection<WatchPrayerLink> WatchPrayerLink { get; set; }
    }
}
