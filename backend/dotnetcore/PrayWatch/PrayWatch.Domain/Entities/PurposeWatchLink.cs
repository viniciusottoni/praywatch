namespace PrayWatch.Domain.Entities
{
    public class PurposeWatchLink
    {
        public int PurposeId { get; set; }
        public PurposeEntity Purpose { get; set; }

        public int WatchId { get; set; }
        public WatchEntity Watch { get; set; }
    }
}
