using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using vidya.Data.Models;
using vidya.Data.Repositories;
using vidya.Services.Data.Games;
using vidya.Web.DTOs.Games;
using vidya.ThirdParty.Services.Images;
using MockQueryable.Moq;
using vidya.Services.Mapping;

namespace vidya.Services.Test
{
    [TestFixture]
    public class GameServiceTests
    {
        private Mock<IRepository<Game>> _gameRepositoryMock;
        private Mock<ICloudinaryService> _cloudinaryServiceMock;
        private IGameService _gameService;

        [SetUp]
        public void SetUp()
        {
            _gameRepositoryMock = new Mock<IRepository<Game>>();
            _cloudinaryServiceMock = new Mock<ICloudinaryService>();

            AutoMapperConfig.RegisterMappings(typeof(GameDTO).Assembly, typeof(DetailGameDTO).Assembly);

            _gameService = new GameService(_gameRepositoryMock.Object, _cloudinaryServiceMock.Object);
        }

        [Test]
        public async Task AddGameAsync_ShouldAddGame()
        {
            // Arrange
            var addGameDTO = new AddGameDTO
            {
                Name = "Test Game",
                Description = "Test Description",
                Price = 49.99m,
                Image = null 
            };

            _gameRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Game>()))
                .Returns(Task.CompletedTask);

            await _gameService.AddGameAsync(addGameDTO);

            _gameRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Game>()), Times.Once);
            _gameRepositoryMock.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task DeleteGame_ShouldDeleteGame()
        {
            var gameId = 1;
            var game = new Game { Id = gameId, Name = "Test Game" };

            _gameRepositoryMock.Setup(repo => repo.AllAsNoTracking())
                .Returns(new List<Game> { game }.AsQueryable().BuildMockDbSet().Object);

            _gameRepositoryMock.Setup(repo => repo.Delete(It.IsAny<Game>()))
                .Verifiable();


            await _gameService.DeleteGame(gameId);

            _gameRepositoryMock.Verify(repo => repo.Delete(It.Is<Game>(g => g.Id == gameId)), Times.Once);
            _gameRepositoryMock.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task ExistsAsync_ShouldReturnTrueIfGameExists()
        {
            var gameId = 1;
            _gameRepositoryMock.Setup(repo => repo.AllAsNoTracking())
                .Returns(new List<Game> { new Game { Id = gameId } }.AsQueryable().BuildMockDbSet().Object);

            var result = await _gameService.ExistsAsync(gameId);

            Assert.That(result == true);
        }

        [Test]
        public async Task ExistsAsync_ShouldReturnFalseIfGameDoesNotExist()
        {
            var gameId = 1;
            _gameRepositoryMock.Setup(repo => repo.AllAsNoTracking())
                .Returns(new List<Game>().AsQueryable().BuildMockDbSet().Object);

            var result = await _gameService.ExistsAsync(gameId);

            Assert.That(result==false);
        }

        [Test]
        public async Task GetDetailGameAsync_ShouldReturnDetailGameDTO()
        {
            var gameId = 1;
            var game = new Game { Id = gameId, Name = "Test Game", Discount = new Discount() };

            _gameRepositoryMock.Setup(repo => repo.AllAsNoTracking())
                .Returns(new List<Game> { game }.AsQueryable().BuildMockDbSet().Object);

            var result = await _gameService.GetDetailGameAsync(gameId);

            Assert.That(result is not null);
            Assert.That(gameId == result.Id);
        }
    }
}
