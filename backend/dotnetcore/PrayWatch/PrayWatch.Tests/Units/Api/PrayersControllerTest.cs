using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PrayWatch.Api.Controllers;
using PrayWatch.Domain.Interfaces;
using PrayWatch.Domain.Models;
using System.Collections.Generic;

namespace PrayWatch.Tests.Units.Api
{
    [TestClass]
    public class PrayersControllerTest
    {
        private readonly Mock<IPrayersService> _mockPrayersService = new Mock<IPrayersService>();
        private readonly PrayersController _prayersController;

        public PrayersControllerTest()
        {
            _prayersController = new PrayersController(_mockPrayersService.Object);
        }

        [TestMethod]
        public void GetAllBy_ReturnPrayers_ByPrayerService()
        {
            // Arrange
            var purpose = "covid19";
            var prayers = new List<PrayerModel>() { new PrayerModel() { Name = "Vinicius Ottoni" } };
            _mockPrayersService.Setup(x => x.GetAllBy(purpose)).Returns(prayers);

            // Act
            var actionResult = _prayersController.GetAllBy(purpose);

            // Assert
            _mockPrayersService.Verify(x => x.GetAllBy(purpose), Times.Once);
            actionResult.Result.Should().BeOfType<OkObjectResult>();
            (actionResult.Result as OkObjectResult).Value.Should().BeEquivalentTo(prayers);
        }
    }
}
