namespace GloboTicket.TicketManagement.Application.Features.Orders.Queries.GetOrdersForMonth
{
    using MediatR;

    public class GetOrdersForMonthQuery : IRequest<PagedOrdersForMonthVm>
    {
        public DateTime Date { get; set; }
        public int Page { get; set; }
        public int Size { get; set; }
    }
}
