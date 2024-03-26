namespace GloboTicket.TicketManagement.Application.Features.Events.Commands.UpdateEvent
{
    using AutoMapper;

    using Contracts.Persistence;
    using Domain.Entities;

    using MediatR;

    public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand>
    {
        private readonly IAsyncRepository<Event> _eventRepository;
        private readonly IMapper _mapper;

        public UpdateEventCommandHandler(
            IMapper mapper, 
            IAsyncRepository<Event> eventRepository)
        {
            this._mapper = mapper;
            this._eventRepository = eventRepository;
        }

        public async Task Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            var eventToUpdate = await this._eventRepository.GetByIdAsync(request.EventId);

            this._mapper.Map(request, eventToUpdate, typeof(UpdateEventCommand), typeof(Event));

            await this._eventRepository.UpdateAsync(eventToUpdate);
        }
    }
}