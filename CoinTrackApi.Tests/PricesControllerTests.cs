using CoinTrackApi.Web.Controllers;
using CoinTrackApi.Application.Commands;
using CoinTrackApi.Application.Dto;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;

namespace CoinTrackApi.Tests
{
    public class PricesControllerTests
    {
        private readonly PricesController _pricesController;

        private readonly Mock<IMediator> _mediator = new Mock<IMediator>();

        public PricesControllerTests()
        {
            _pricesController = new PricesController(_mediator.Object);
        }

        [Test]
        public async Task ShouldReturn200WhenCoinPriceQuerySucceeds()
        {
            // Arrange
            _mediator.Setup(m => m.Send(It.IsAny<GetCoinPriceCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(
                    new CommandResult<CoinPriceDto>(new CoinPriceDto()));
            // Act
            var actionResult = await _pricesController.GetCoinPrice("BTC") as ObjectResult;

            // Assert
            Assert.NotNull(actionResult);
            Assert.AreEqual(StatusCodes.Status200OK, actionResult.StatusCode.Value);
        }

        [Test]
        public async Task ShouldReturn500WhenCoinPriceQueryFails()
        {
            // Arrange
            _mediator.Setup(m => m.Send(It.IsAny<GetCoinPriceCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(
                    new CommandResult<CoinPriceDto>("Failed"));
            // Act
            var actionResult = await _pricesController.GetCoinPrice("BTC") as ObjectResult;

            // Assert
            Assert.NotNull(actionResult);
            Assert.AreEqual(StatusCodes.Status500InternalServerError, actionResult.StatusCode.Value);
        }
    }
}