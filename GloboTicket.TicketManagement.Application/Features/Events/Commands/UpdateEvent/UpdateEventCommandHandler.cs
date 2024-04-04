namespace GloboTicket.TicketManagement.Application.Features.Events.Commands.UpdateEvent
{
    using AutoMapper;

    using Contracts.Persistence;
    using Domain.Entities;

    using Exceptions;

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
            
            if (eventToUpdate == null) 
            {
                throw new NotFoundException(nameof(Event), request.EventId);
            }

            var validator = new UpdateEventCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
                throw new ValidationException(validationResult);

            this._mapper.Map(request, eventToUpdate, typeof(UpdateEventCommand), typeof(Event));

            await this._eventRepository.UpdateAsync(eventToUpdate);
        }
    }
}