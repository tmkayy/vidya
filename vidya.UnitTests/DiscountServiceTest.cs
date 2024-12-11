using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using vidya.Data.Models;
using vidya.Data.Repositories;
using vidya.Services.Data.Discounts;
using vidya.Web.DTOs.Discounts;
using MockQueryable.Moq;
using vidya.Services.Mapping;

namespace vidya.Services.Tests
{
    [TestFixture]
    public class DiscountServiceTest
    {
        private Mock<IRepository<Discount>> _repositoryDiscountMock;
        private Mock<IRepository<Game>> _repositoryGameMock;
        private IDiscountService _discountService;

        [SetUp]
        public void SetUp()
        {
            _repositoryDiscountMock = new Mock<IRepository<Discount>>();
            _repositoryGameMock = new Mock<IRepository<Game>>();

            AutoMapperConfig.RegisterMappings(typeof(AddDiscountDTO).Assembly);

            _discountService = new DiscountService(_repositoryDiscountMock.Object, _repositoryGameMock.Object);
        }

        [Test]
        public async Task AddDiscountAsync_ShouldAddDiscountIfGameExists()
        {
            var game = new Game { Id = 1, Name = "Test Game" };
            var addDiscountDTO = new AddDiscountDTO { Id = 1, Percentage = 20 };

            _repositoryGameMock.Setup(repo => repo.All())
                .Returns(new List<Game> { game }.AsQueryable().BuildMockDbSet().Object);
            _repositoryDiscountMock.Setup(repo => repo.All())
                .Returns(new List<Discount>().AsQueryable().BuildMockDbSet().Object);

            await _discountService.AddDiscountAsync(addDiscountDTO, 1);

            _repositoryDiscountMock.Verify(repo => repo.AddAsync(It.IsAny<Discount>()), Times.Once);
            _repositoryDiscountMock.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task AddDiscountAsync_ShouldAddGameToExistingDiscountIfGameAlreadyAssigned()
        {
            var game = new Game { Id = 1, Name = "Test Game" };
            var discount = new Discount { Id = 1, Games = new List<Game> { game } };
            var addDiscountDTO = new AddDiscountDTO { Id = 1, Percentage = 20 };

            _repositoryGameMock.Setup(repo => repo.All())
                .Returns(new List<Game> { game }.AsQueryable().BuildMockDbSet().Object);
            _repositoryDiscountMock.Setup(repo => repo.All())
                .Returns(new List<Discount> { discount }.AsQueryable().BuildMockDbSet().Object);

            await _discountService.AddDiscountAsync(addDiscountDTO, 1);

            _repositoryDiscountMock.Verify(repo => repo.SaveChangesAsync(), Times.Once);
            Assert.That(discount.Games.Count, Is.EqualTo(1));  // The game should not be duplicated
        }

        [Test]
        public async Task AddDiscountAsync_ShouldThrowExceptionIfGameDoesNotExist()
        {
            var addDiscountDTO = new AddDiscountDTO { Id = 1, Percentage = 20 };

            _repositoryGameMock.Setup(repo => repo.All())
                .Returns(new List<Game>().AsQueryable().BuildMockDbSet().Object); // No games available

            Assert.ThrowsAsync<ArgumentException>(async () =>
                await _discountService.AddDiscountAsync(addDiscountDTO, 1));
        }

        [Test]
        public void CalculateDiscountedPrice_ShouldReturnCorrectDiscountedPrice()
        {
            var price = 100m;
            var percentage = 20m;

            var result = _discountService.CalculateDiscountedPrice(price, percentage);

            Assert.That(80m == result);
        }

        [Test]
        public async Task DeleteExpiredDiscountsAsync_ShouldDeleteExpiredDiscounts()
        {
            var expiredDiscounts = new List<Discount>
            {
                new Discount { Id = 1, EndDate = DateTime.UtcNow.AddDays(-1) },
                new Discount { Id = 2, EndDate = DateTime.UtcNow.AddDays(-2) }
            };

            _repositoryDiscountMock.Setup(repo => repo.All())
                .Returns(expiredDiscounts.AsQueryable().BuildMockDbSet().Object);

            await _discountService.DeleteExpiredDiscountsAsync();

            _repositoryDiscountMock.Verify(repo => repo.Delete(It.IsAny<Discount>()), Times.Exactly(2));
            _repositoryDiscountMock.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }
    }
}
