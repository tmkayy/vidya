using Moq;
using MockQueryable.EntityFrameworkCore;
using vidya.Data.Models;
using vidya.Data.Repositories;
using vidya.Services.Data.ActivationKeys;
using vidya.Web.DTOs.ActivationKeys;
using vidya.Services.Mapping;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;

[TestFixture]
public class ActivationKeyServiceTests
{
    private Mock<IRepository<ActivationKey>> _activationKeyRepositoryMock;
    private Mock<IRepository<LocationsKeys>> _locationsKeysRepositoryMock;
    private IActivationKeyService _activationKeyService;

    [SetUp]
    public void SetUp()
    {
        _activationKeyRepositoryMock = new Mock<IRepository<ActivationKey>>();
        _locationsKeysRepositoryMock = new Mock<IRepository<LocationsKeys>>();

        AutoMapperConfig.RegisterMappings(typeof(ActivationKeyDTO).Assembly);

        _activationKeyService = new ActivationKeyService(_activationKeyRepositoryMock.Object, _locationsKeysRepositoryMock.Object);
    }

    [Test]
    public async Task AddActivationKeyAsync_ShouldAddKeyAndLocations()
    {
        var addKeyDto = new AddActivationKeyDTO
        {
            Key = "TEST-KEY-123",
            SelectedIds = new List<int> { 1, 2, 3 },
            GameId = 1
        };

        _activationKeyRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<ActivationKey>())).Returns(Task.CompletedTask);
        _locationsKeysRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<LocationsKeys>())).Returns(Task.CompletedTask);
        await _activationKeyService.AddActivationKeyAsync(addKeyDto);

        _activationKeyRepositoryMock.Verify(repo => repo.AddAsync(It.Is<ActivationKey>(ak => ak.Key == "TEST-KEY-123" && ak.GameId == 1)), Times.Once);
        _locationsKeysRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<LocationsKeys>()), Times.Exactly(3));
        _activationKeyRepositoryMock.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        _locationsKeysRepositoryMock.Verify(repo => repo.SaveChangesAsync(), Times.Once);
    }


    [Test]
    public async Task ExistsAsync_ShouldReturnTrueIfExists()
    {
        var activationKey = new ActivationKey { Id = 1 };
        _activationKeyRepositoryMock.Setup(repo => repo.AllAsNoTracking())
            .Returns(new List<ActivationKey> { activationKey }.AsQueryable().BuildMockDbSet().Object);

        var result = await _activationKeyService.ExistsAsync(1);

        Assert.That(result, Is.True);
    }

    [Test]
    public async Task ExistsAsync_ShouldReturnFalseIfNotExists()
    {
        _activationKeyRepositoryMock.Setup(repo => repo.AllAsNoTracking())
            .Returns(new List<ActivationKey>().AsQueryable().BuildMockDbSet().Object);

        var result = await _activationKeyService.ExistsAsync(1);

        Assert.That(result, Is.False);
    }

    [Test]
    public async Task GetBoughtKeysAsync_ShouldReturnKeysForUser()
    {
        var game = new Game { Id = 1, Name = "Test Game" }; // Mock the Game entity
        var locations = new List<Location> { new Location { Id = 1, Continent = "Location 1" }, new Location { Id = 2, Continent = "Location 2" } };

        var activationKeys = new List<ActivationKey>
    {
        new ActivationKey { Id = 1, UserId = "user1", GameId = 1, Game = game, Locations = new List<LocationsKeys>
            {
                new LocationsKeys { Location = locations[0] },
                new LocationsKeys { Location = locations[1] }
            }
        },
        new ActivationKey { Id = 2, UserId = "user1", GameId = 1, Game = game, Locations = new List<LocationsKeys>
            {
                new LocationsKeys { Location = locations[0] }
            }
        }
    };

        _activationKeyRepositoryMock.Setup(repo => repo.AllAsNoTracking())
            .Returns(activationKeys.AsQueryable().BuildMockDbSet().Object);

        var result = await _activationKeyService.GetBoughtKeysAsync("user1");

        Assert.That(result.Count(), Is.EqualTo(2));
    }

}
