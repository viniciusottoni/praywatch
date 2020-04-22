using PrayWatch.Domain.Models;
using System.Collections.Generic;

namespace PrayWatch.Domain.Interfaces
{
    public interface IPurposesService
    {
        PurposeModel GetBy(string purpose);
    }
}
