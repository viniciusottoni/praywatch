using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PrayWatch.Domain.Entities;
using PrayWatch.Domain.Interfaces;
using PrayWatch.Domain.Models;
using PrayWatch.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PrayWatch.Tests.Units.Domain.Services
{
    [TestClass]
    public class PurposesServiceTest
    {
        private readonly Mock<IPurposesRepository> _mockPurposesRepository = new Mock<IPurposesRepository>();
        private readonly PurposesService _purposesService;

        public PurposesServiceTest()
        {
            _purposesService = new PurposesService(_mockPurposesRepository.Object);
        }

        public void GetAllBy_ReturnNull_IfEntityIsNull()
        {
            // Arrange
            string purposeName = "covid19";
            _mockPurposesRepository.Setup(x => x.GetBy(purposeName)).Returns((PurposeEntity)null);

            // Act
            var result = _purposesService.GetBy(purposeName);

            // Assert
            result.Should().BeNull();
        }

        [TestMethod]
        public void GetAllBy_ReturnPurpose_ByPurposesRepository()
        {
            // Arrange
            string purposeName = "covid19";
            PurposeEntity entity = new PurposeEntity()
            {
                Name = "Covid19",
                PurposeWatchLink = new List<PurposeWatchLink>()
                {
                    new PurposeWatchLink()
                    {
                        Watch = new WatchEntity()
                        {
                            TimeRange = 0,
                            WatchPrayerLink = new List<WatchPrayerLink>()
                        }
                    },
                    new PurposeWatchLink()
                    {
                        Watch = new WatchEntity()
                        {
                            TimeRange = 5,
                            WatchPrayerLink = new List<WatchPrayerLink>()
                            {
                                new WatchPrayerLink()
                                {
                                    Prayer = new PrayerEntity()
                                    {
                                        Id = Guid.NewGuid(),
                                        FullName = "Vinicius Ottoni",
                                        Country = new CountryEntity()
                                        {
                                            IsoCode = "Brazil"
                                        }
                                    }
                                },
                                new WatchPrayerLink()
                                {
                                    Prayer = new PrayerEntity()
                                    {
                                        Id = Guid.NewGuid(),
                                        FullName = "Raphael Ribeiro",
                                        Country = new CountryEntity()
                                        {
                                            IsoCode = "Brazil"
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };
            var purpose = new PurposeModel()
            {
                Name = entity.Name,
                PrayWatch = entity.PurposeWatchLink
                    .Select(x => x.Watch).ToList().ConvertAll(watch => new WatchModel()
                    {
                        TimeRange = watch.TimeRange,
                        Prayers = watch.WatchPrayerLink
                            .Select(x => x.Prayer).ToList().ConvertAll(prayer => new PrayerModel()
                            {
                                Id = prayer.Id,
                                Name = prayer.FullName,
                                Country = prayer.Country.IsoCode
                            })
                    })
            };
            _mockPurposesRepository.Setup(x => x.GetBy(purposeName)).Returns(entity);

            // Act
            var result = _purposesService.GetBy(purposeName);

            // Assert
            result.Should().BeEquivalentTo(purpose);
        }
    }
}
