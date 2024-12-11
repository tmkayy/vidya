using MockQueryable.Moq;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using vidya.Data.Models;
using vidya.Data.Repositories;
using vidya.Services.Data.Users;

namespace vidya.Services.Tests
{
    [TestFixture]
    public class UserServiceTests
    {
        private Mock<IRepository<ApplicationUser>> _userRepositoryMock;
        private IUserService _userService;

        [SetUp]
        public void SetUp()
        {
            _userRepositoryMock = new Mock<IRepository<ApplicationUser>>();
            _userService = new UserService(_userRepositoryMock.Object);
        }

        [Test]
        public async Task GetUserIdByEmail_ShouldReturnUserId_WhenUserExists()
        {
            var email = "testuser@example.com";
            var expectedUserId = "user123";
            var user = new ApplicationUser { Id = expectedUserId, Email = email };

            _userRepositoryMock.Setup(repo => repo.AllAsNoTracking())
                .Returns(new[] { user }.AsQueryable().BuildMockDbSet().Object);

            var result = await _userService.GetUserIdByEmail(email);

            Assert.That(expectedUserId == result);
            _userRepositoryMock.Verify(repo => repo.AllAsNoTracking(), Times.Once);
        }
    }
}
