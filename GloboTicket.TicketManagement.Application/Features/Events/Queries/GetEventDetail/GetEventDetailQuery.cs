namespace GloboTicket.TicketManagement.Application.Features.Events.Queries.GetEventDetail
{
    using MediatR;
    public class GetEventDetailQuery : IRequest<EventDetailVm>
    {
        public Guid Id { get; set; }
    }
}
