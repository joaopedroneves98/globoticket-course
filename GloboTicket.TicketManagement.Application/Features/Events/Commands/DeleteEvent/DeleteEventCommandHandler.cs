namespace GloboTicket.TicketManagement.Application.Features.Events.Commands.DeleteEvent
{
    using AutoMapper;

    using Contracts.Persistence;

    using Domain.Entities;

    using MediatR;

    public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand>
    {
        private readonly IAsyncRepository<Event> _eventRepository;
        private readonly IMapper _mapper;

        public DeleteEventCommandHandler(
            IMapper mapper, 
            IAsyncRepository<Event> eventRepository)
        {
            this._mapper = mapper;
            this._eventRepository = eventRepository;
        }

        public async Task Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            var eventToDelete = await this._eventRepository.GetByIdAsync(request.EventId);

            await this._eventRepository.DeleteAsync(eventToDelete);
        }
    }
}