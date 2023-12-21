using CoinTrackApi.Web.Controllers;
using CoinTrackApi.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;
using CoinDtos = System.Collections.Generic.List<CoinTrackApi.Application.Dto.CoinDto>;

namespace CoinTrackApi.Tests
{
    public class CoinsControllerTests
    {
        private readonly CoinsController _coinsController;

        private readonly Mock<IMediator> _mediator = new Mock<IMediator>();

        public CoinsControllerTests()
        {
            _coinsController = new CoinsController(_mediator.Object);
        }

        [Test]
        public async Task ShouldReturn200WhenCoinsQuerySucceeds()
        {
            // Arrange
            _mediator.Setup(m => m.Send(It.IsAny<GetCoinsCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(
                    new CommandResult<CoinDtos>(new CoinDtos()));
            // Act
            var actionResult = await _coinsController.GetCoins() as ObjectResult;

            // Assert
            Assert.NotNull(actionResult);
            Assert.AreEqual(StatusCodes.Status200OK, actionResult.StatusCode.Value);
        }


        [Test]
        public async Task ShouldReturn500WhenCoinsQueryFails()
        {
            // Arrange
            _mediator.Setup(m => m.Send(It.IsAny<GetCoinsCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(
                    new CommandResult<CoinDtos>("Failed"));
            // Act
            var actionResult = await _coinsController.GetCoins() as ObjectResult;

            // Assert
            Assert.NotNull(actionResult);
            Assert.AreEqual(StatusCodes.Status500InternalServerError, actionResult.StatusCode.Value);
        }
    }
}