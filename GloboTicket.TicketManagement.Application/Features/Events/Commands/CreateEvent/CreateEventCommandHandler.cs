namespace GloboTicket.TicketManagement.Application.Features.Events.Commands.CreateEvent
{
    using AutoMapper;

    using Contracts.Infrastructure;
    using Contracts.Persistence;

    using Domain.Entities;

    using Exceptions;

    using MediatR;

    using Models.Mail;

    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, Guid>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;

        public CreateEventCommandHandler(
            IMapper mapper,
            IEventRepository eventRepository,
            IEmailService emailService)
        {
            this._mapper = mapper;
            this._eventRepository = eventRepository;
            this._emailService = emailService;
        }

        public async Task<Guid> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            var @event = this._mapper.Map<Event>(request);

            var validator = new CreateEventCommandValidator(this._eventRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
            {
                throw new ValidationException(validationResult);
            }

            @event = await this._eventRepository.AddAsync(@event);

            //Sending email notification to admin address
            var email = new Email()
            {
                To = "gill@snowball.be",
                Body = $"A new event was created: {request}",
                Subject = "A new event was created"
            };

            try
            {
                await _emailService.SendEmail(email);
            }
            catch (Exception ex)
            {
                //this shouldn't stop the API from doing else so this can be logged
            }

            return @event.EventId;
        }
    }
}