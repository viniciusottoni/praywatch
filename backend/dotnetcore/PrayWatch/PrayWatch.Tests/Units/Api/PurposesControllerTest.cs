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
    public class PurposesControllerTest
    {
        private readonly Mock<IPurposesService> _mockPurposesService = new Mock<IPurposesService>();
        private readonly PurposesController _purposesController;

        public PurposesControllerTest()
        {
            _purposesController = new PurposesController(_mockPurposesService.Object);
        }

        [TestMethod]
        public void GetPrayWatch_ReturnPrayWatch_ByPurposesService()
        {
            // Arrange
            var purposeName = "covid19";
            var purpose = new PurposeModel()
            {
                Name = "Covid19",
                PrayWatch = new List<WatchModel>() { new WatchModel() { TimeRange = 0 } }
            };
            _mockPurposesService.Setup(x => x.GetBy(purposeName)).Returns(purpose);

            // Act
            var actionResult = _purposesController.GetBy(purposeName);

            // Assert
            _mockPurposesService.Verify(x => x.GetBy(purposeName), Times.Once);
            actionResult.Result.Should().BeOfType<OkObjectResult>();
            (actionResult.Result as OkObjectResult).Value.Should().BeEquivalentTo(purpose);
        }
    }
}
