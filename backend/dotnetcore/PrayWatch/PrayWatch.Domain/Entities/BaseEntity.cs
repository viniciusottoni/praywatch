using System;

namespace PrayWatch.Domain.Entities
{
    public class BaseEntity
    {
        public DateTime CreatedDate { get; set; }
        public bool Deleted { get; set; }
    }
}
