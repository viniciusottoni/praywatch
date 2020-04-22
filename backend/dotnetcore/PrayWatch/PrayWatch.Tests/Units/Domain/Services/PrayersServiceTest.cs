using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PrayWatch.Domain.Entities;
using PrayWatch.Domain.Interfaces;
using PrayWatch.Domain.Models;
using PrayWatch.Domain.Services;
using System;
using System.Collections.Generic;

namespace PrayWatch.Tests.Units.Domain.Services
{
    [TestClass]
    public class PrayersServiceTest
    {
        private readonly Mock<IPrayersRepository> _mockPrayersRepository = new Mock<IPrayersRepository>();
        private readonly PrayersService _prayersService;

        public PrayersServiceTest()
        {
            _prayersService = new PrayersService(_mockPrayersRepository.Object);
        }

        [TestMethod]
        public void GetAllBy_ReturnEmptyList_IfEntitiesIsNull()
        {
            // Arrange
            var purpose = "covid19";
            _mockPrayersRepository.Setup(x => x.GetAllBy(purpose)).Returns((List<PrayerEntity>)null);

            // Act
            var result = _prayersService.GetAllBy(purpose);

            // Assert
            result.Should().BeEmpty();
        }

       [TestMethod]
        public void GetAllBy_ReturnPrayers_ByPrayersRepository()
        {
            // Arrange
            var purpose = "covid";
            var entities = new List<PrayerEntity>()
            {
                new PrayerEntity()
                {
                    Id = Guid.NewGuid(),
                    FullName = "Vinicius Ottoni",
                    Country = new CountryEntity()
                    {
                        IsoCode = "Brazil"
                    },
                },
                new PrayerEntity()
                {
                    Id = Guid.NewGuid(),
                    FullName = "Raphael Ribeiro",
                    Country = new CountryEntity()
                    {
                        IsoCode = "Brazil"
                    },
                }
            };
            var prayers = entities.ConvertAll(x => new PrayerModel()
            {
                Id = x.Id,
                Name = x.FullName,
                Country = x.Country.IsoCode,
            });
            _mockPrayersRepository.Setup(x => x.GetAllBy(purpose)).Returns(entities);

            // Act
            var result = _prayersService.GetAllBy(purpose);

            // Assert
            result.Should().BeEquivalentTo(prayers);
        }
    }
}
