using System.Collections.Generic;

namespace PrayWatch.Domain.Models
{
    public class PurposeModel
    {
        public string Name { get; set; }
        public List<WatchModel> PrayWatch { get; set; }
    }
}
