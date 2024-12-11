using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using vidya.Data.Models;
using vidya.Data.Repositories;
using vidya.Services.Data.Locations;
using vidya.Web.DTOs.Locations;
using MockQueryable.Moq;
using vidya.Services.Mapping;

namespace vidya.Services.Tests
{
    [TestFixture]
    public class LocationServiceTest
    {
        private Mock<IRepository<Location>> _locationRepositoryMock;
        private ILocationService _locationService;

        [SetUp]
        public void SetUp()
        {
            _locationRepositoryMock = new Mock<IRepository<Location>>();

            AutoMapperConfig.RegisterMappings(typeof(LocationDTO).Assembly);

            _locationService = new LocationService(_locationRepositoryMock.Object);
        }

        [Test]
        public async Task GetLocationsAsync_ShouldReturnLocations()
        {
            var locations = new List<Location>
            {
                new Location { Id = 1, Continent = "Asia" },
                new Location { Id = 2, Continent = "Europe" }
            };

            _locationRepositoryMock.Setup(repo => repo.AllAsNoTracking())
                .Returns(locations.AsQueryable().BuildMockDbSet().Object);

            var result = await _locationService.GetLocationsAsync();

            Assert.That(2 == result.Count());
            Assert.That("Asia" == result.First().Continent);
            Assert.That("Europe" == result.Last().Continent);
        }

        [Test]
        public async Task GetLocationsAsync_ShouldReturnEmptyIfNoLocations()
        {
            _locationRepositoryMock.Setup(repo => repo.AllAsNoTracking())
                .Returns(new List<Location>().AsQueryable().BuildMockDbSet().Object);

            var result = await _locationService.GetLocationsAsync();

            Assert.That(0 == result.Count());
        }
    }
}
