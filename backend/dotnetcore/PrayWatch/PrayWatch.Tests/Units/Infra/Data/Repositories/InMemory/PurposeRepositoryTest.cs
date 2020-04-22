using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrayWatch.Infra.Data.Contexts;
using PrayWatch.Infra.Data.Repositories.InMemory;
using System.Linq;

namespace PrayWatch.Tests.Units.Infra.Data.Repositories.InMemory
{
    [TestClass]
    public class PurposeRepositoryTest
    {
        private readonly PrayWatchDbContext _context;
        private readonly PurposesRepository _purposesRepository;

        public PurposeRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<PrayWatchDbContext>()
                .UseInMemoryDatabase(databaseName: "PrayWatch")
                .Options;
            _context = new PrayWatchDbContext(options);
            DataGenerator.Initialize(_context);

            _purposesRepository = new PurposesRepository(_context);
        }

        [TestMethod]
        public void GetBy_ReturnNull_WhenDoesntFindPurposeOrIsDeletedInMemory()
        {
            // Act
            var entity1 = _purposesRepository.GetBy("zika");
            var entity2 = _purposesRepository.GetBy("ebola");

            // Assert
            entity1.Should().BeNull();
            entity2.Should().BeNull();
        }

        [TestMethod]
        public void GetBy_ReturnData_WhenFindPurposeInMemory()
        {
            // Act
            var entity = _purposesRepository.GetBy("covid19");

            // Assert
            entity.Name.Should().Be("Covid19");

            var watches = entity.PurposeWatchLink.Select(x => x.Watch).ToList();
            watches.FirstOrDefault(x => x.TimeRange == 0).Should().NotBeNull();
            watches.FirstOrDefault(x => x.TimeRange == 5).Should().NotBeNull();
            watches.FirstOrDefault(x => x.TimeRange == 10).Should().NotBeNull();

            var prayersWatch0 = watches.First(x => x.TimeRange == 0).WatchPrayerLink.Select(x => x.Prayer).ToList();
            prayersWatch0.FirstOrDefault(x => x.FullName == "Vinicius Ottoni" && x.Country.IsoCode == "BRA").Should().NotBeNull();

            var prayersWatch5 = watches.First(x => x.TimeRange == 5).WatchPrayerLink.Select(x => x.Prayer).ToList();
            prayersWatch5.FirstOrDefault(x => x.FullName == "Vinicius Ottoni" && x.Country.IsoCode == "BRA").Should().NotBeNull();
            prayersWatch5.FirstOrDefault(x => x.FullName == "Raphael Ribeiro" && x.Country.IsoCode == "USA").Should().NotBeNull();

            var prayersWatch10 = watches.First(x => x.TimeRange == 10).WatchPrayerLink.Select(x => x.Prayer).ToList();
            prayersWatch10.FirstOrDefault(x => x.FullName == "Raphael Ribeiro" && x.Country.IsoCode == "USA").Should().NotBeNull();
        }
    }
}
