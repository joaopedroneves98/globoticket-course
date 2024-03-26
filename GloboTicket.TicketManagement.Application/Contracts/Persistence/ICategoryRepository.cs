namespace GloboTicket.TicketManagement.Application.Contracts.Persistence
{
    using Domain.Entities;

    public interface ICategoryRepository : IAsyncRepository<Category>
    {
        Task<List<Category>> GetCategoriesWithEvents(bool includePassedEvents);
    }
}