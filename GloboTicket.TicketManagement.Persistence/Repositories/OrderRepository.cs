namespace GloboTicket.TicketManagement.Persistence.Repositories
{
    using Application.Contracts.Persistence;

    using Domain.Entities;

    using Microsoft.EntityFrameworkCore;

    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(GloboTicketDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Order>> GetPagedOrdersForMonth(DateTime date, int page, int size)
        {
            return await this._dbContext.Orders.Where(x => x.OrderPlaced.Month == date.Month && x.OrderPlaced.Year == date.Year)
                .Skip((page - 1) * size).Take(size).AsNoTracking().ToListAsync();
        }

        public async Task<int> GetTotalCountOfOrdersForMonth(DateTime date)
        {
            return await this._dbContext.Orders.CountAsync(x => x.OrderPlaced.Month == date.Month && x.OrderPlaced.Year == date.Year);
        }
    }
}