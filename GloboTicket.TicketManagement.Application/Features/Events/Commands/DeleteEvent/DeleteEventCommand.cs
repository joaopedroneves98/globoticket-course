namespace GloboTicket.TicketManagement.Application.Features.Events.Commands.DeleteEvent
{
    using MediatR;

    public class DeleteEventCommand : IRequest
    {
        public Guid EventId { get; set; }
    }
}