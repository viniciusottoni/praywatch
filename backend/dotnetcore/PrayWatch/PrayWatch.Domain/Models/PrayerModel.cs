using System;

namespace PrayWatch.Domain.Models
{
    public class PrayerModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
    }
}
