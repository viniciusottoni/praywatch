using System.Collections.Generic;

namespace PrayWatch.Domain.Entities
{
    public class PurposeEntity : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<PurposeWatchLink> PurposeWatchLink { get; set; }
    }
}
