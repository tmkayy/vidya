using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using vidya.Data.Models;
using vidya.Data.Repositories;
using vidya.Services.Data.SupportTickets;
using vidya.Web.DTOs.SupportTickets;
using MockQueryable.Moq;
using vidya.Services.Mapping;

namespace vidya.Services.Tests
{
    [TestFixture]
    public class SupportTicketServiceTest
    {
        private Mock<IRepository<SupportTicket>> _repositoryMock;
        private ISupportTicketService _supportTicketService;

        [SetUp]
        public void SetUp()
        {
            _repositoryMock = new Mock<IRepository<SupportTicket>>();

            AutoMapperConfig.RegisterMappings(typeof(SupportTicketPagedDTO).Assembly);

            _supportTicketService = new SupportTicketService(_repositoryMock.Object);
        }

        [Test]
        public async Task SendTicketAsync_ShouldAddTicket()
        {
            var sendTicketDTO = new SendTicketDTO
            {
                Title = "Test Ticket",
                Content = "Test Description"
            };
            var userId = "user123";

            _repositoryMock.Setup(repo => repo.AddAsync(It.IsAny<SupportTicket>())).Returns(Task.CompletedTask);

            await _supportTicketService.SendTicketAsync(sendTicketDTO, userId);

            _repositoryMock.Verify(repo => repo.AddAsync(It.Is<SupportTicket>(st =>
                st.Title == "Test Ticket" && st.Content == "Test Description" && st.UserId == "user123")), Times.Once);
            _repositoryMock.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task ResolveTicketAsync_ShouldSetTicketToResolved()
        {
            var ticketId = 1;
            var supportTicket = new SupportTicket { Id = ticketId, IsResolved = false };
            _repositoryMock.Setup(repo => repo.All()).Returns(new List<SupportTicket> { supportTicket }.AsQueryable().BuildMockDbSet().Object);

            await _supportTicketService.ResolveTicketAsync(ticketId);

            Assert.That(supportTicket.IsResolved == true);
            _repositoryMock.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task GetPagedTicketsAsync_ShouldReturnPagedTickets()
        {
            var tickets = new List<SupportTicket>
            {
                new SupportTicket { Id = 1, Title = "Ticket 1", IsResolved = false, User = new ApplicationUser() },
                new SupportTicket { Id = 2, Title = "Ticket 2", IsResolved = false, User = new ApplicationUser() },
                new SupportTicket { Id = 3, Title = "Ticket 3", IsResolved = false, User = new ApplicationUser() }
            };
            _repositoryMock.Setup(repo => repo.AllAsNoTracking()).Returns(tickets.AsQueryable().BuildMockDbSet().Object);

            var result = await _supportTicketService.GetPagedTicketsAsync(1, 2);

            Assert.That(2 == result.Tickets.Count());
            Assert.That(2 == result.TotalPages);
            Assert.That(1 == result.CurrentPage);
        }

        [Test]
        public async Task ExistsAsync_ShouldReturnTrueIfTicketExists()
        {
            var ticketId = 1;
            var supportTicket = new SupportTicket { Id = ticketId };
            _repositoryMock.Setup(repo => repo.AllAsNoTracking())
                .Returns(new List<SupportTicket> { supportTicket }.AsQueryable().BuildMockDbSet().Object);

            var result = await _supportTicketService.ExistsAsync(ticketId);

            Assert.That(result == true);
        }

        [Test]
        public async Task ExistsAsync_ShouldReturnFalseIfTicketDoesNotExist()
        {
            _repositoryMock.Setup(repo => repo.AllAsNoTracking())
                .Returns(new List<SupportTicket>().AsQueryable().BuildMockDbSet().Object);

            var result = await _supportTicketService.ExistsAsync(1);

            Assert.That(result == false);
        }
    }
}
