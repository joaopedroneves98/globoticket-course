namespace GloboTicket.TicketManagement.Application.Contracts.Persistence
{
    using Domain.Entities;

    public interface IOrderRepository : IAsyncRepository<Order>
    {
    }
}