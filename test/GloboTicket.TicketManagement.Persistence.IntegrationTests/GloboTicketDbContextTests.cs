namespace GloboTicket.TicketManagement.Persistence.IntegrationTests
{
    using Application.Contracts;
    using Domain.Entities;

    using Microsoft.EntityFrameworkCore;

    using Moq;

    using Shouldly;

    public class GloboTicketDbContextTests
    {
        private readonly GloboTicketDbContext _globoTicketDbContext;
        private readonly Mock<ILoggedInUserService> _loggedInUserServiceMock;
        private readonly string _loggedInUserId;

        public GloboTicketDbContextTests()
        {
            var dbContextOptions = new DbContextOptionsBuilder<GloboTicketDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            this._loggedInUserId = "00000000-0000-0000-0000-000000000000";
            this._loggedInUserServiceMock = new Mock<ILoggedInUserService>();
            this._loggedInUserServiceMock.Setup(m => m.UserId).Returns(this._loggedInUserId);

            this._globoTicketDbContext = new GloboTicketDbContext(dbContextOptions, this._loggedInUserServiceMock.Object);
        }

        [Fact]
        public async void Save_SetCreatedByProperty()
        {
            var ev = new Event()
            {
                EventId = Guid.NewGuid(),
                Name = "Test event"
            };

            this._globoTicketDbContext.Events.Add(ev);
            await this._globoTicketDbContext.SaveChangesAsync();

            ev.CreatedBy.ShouldBe(this._loggedInUserId);
        }
    }
}