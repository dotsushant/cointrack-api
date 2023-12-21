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
    public class SettingsControllerTests
    {
        private readonly SettingsController _settingsController;

        private readonly Mock<IMediator> _mediator = new Mock<IMediator>();

        public SettingsControllerTests()
        {
            _settingsController = new SettingsController(_mediator.Object);
        }

        [Test]
        public async Task ShouldReturn200WhenCoinSettingQuerySucceeds()
        {
            // Arrange
            _mediator.Setup(m => m.Send(It.IsAny<GetCoinSettingCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(
                    new CommandResult<CoinSettingDto>(new CoinSettingDto
                    {
                        Symbol = "BTC",
                    }));
            // Act
            var actionResult = await _settingsController.GetCoinSetting() as ObjectResult;

            // Assert
            Assert.NotNull(actionResult);
            Assert.AreEqual(StatusCodes.Status200OK, actionResult.StatusCode.Value);
        }

        [Test]
        public async Task ShouldReturn500WhenCoinSettingQueryFails()
        {
            // Arrange
            _mediator.Setup(m => m.Send(It.IsAny<GetCoinSettingCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(
                    new CommandResult<CoinSettingDto>("Failed"));
            // Act
            var actionResult = await _settingsController.GetCoinSetting() as ObjectResult;

            // Assert
            Assert.NotNull(actionResult);
            Assert.AreEqual(StatusCodes.Status500InternalServerError, actionResult.StatusCode.Value);
        }

        [Test]
        public async Task ShouldReturn200WhenCoinSettingUpdateSucceeds()
        {
            // Arrange
            _mediator.Setup(m => m.Send(It.IsAny<UpdateCoinSettingCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(
                    new CommandResult<bool>(true));
            // Act
            var actionResult = await _settingsController.UpdateCoinSetting(new CoinSettingDto { Symbol = "BTC" }) as ObjectResult;

            // Assert
            Assert.NotNull(actionResult);
            Assert.AreEqual(StatusCodes.Status200OK, actionResult.StatusCode.Value);
        }

        [Test]
        public async Task ShouldReturn500WhenCoinSettingUpdateFails()
        {
            // Arrange
            _mediator.Setup(m => m.Send(It.IsAny<UpdateCoinSettingCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(
                    new CommandResult<bool>("Failed"));
            // Act
            var actionResult = await _settingsController.UpdateCoinSetting(new CoinSettingDto { Symbol = "BTC" }) as ObjectResult;

            // Assert
            Assert.NotNull(actionResult);
            Assert.AreEqual(StatusCodes.Status500InternalServerError, actionResult.StatusCode.Value);
        }
    }
}