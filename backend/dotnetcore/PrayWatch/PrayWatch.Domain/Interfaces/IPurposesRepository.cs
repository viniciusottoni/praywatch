using PrayWatch.Domain.Entities;

namespace PrayWatch.Domain.Interfaces
{
    public interface IPurposesRepository
    {
        PurposeEntity GetBy(string purpose);
    }
}
