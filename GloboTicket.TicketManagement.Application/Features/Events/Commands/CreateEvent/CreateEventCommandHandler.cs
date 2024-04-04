namespace GloboTicket.TicketManagement.Application.Features.Events.Commands.CreateEvent
{
    using AutoMapper;

    using Contracts.Infrastructure;
    using Contracts.Persistence;

    using Domain.Entities;

    using Exceptions;

    using MediatR;

    using Microsoft.Extensions.Logging;

    using Models.Mail;

    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, Guid>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ILogger<CreateEventCommandHandler> _logger;

        public CreateEventCommandHandler(
            IMapper mapper,
            IEventRepository eventRepository,
            IEmailService emailService, 
            ILogger<CreateEventCommandHandler> logger)
        {
            this._mapper = mapper;
            this._eventRepository = eventRepository;
            this._emailService = emailService;
            this._logger = logger;
        }

        public async Task<Guid> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateEventCommandValidator(this._eventRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
                throw new ValidationException(validationResult);

            var @event = this._mapper.Map<Event>(request);


            @event = await this._eventRepository.AddAsync(@event);


            var email = new Email()
            {
                To = "gill@snowball.be",
                Body = $"A new event was created: {request}",
                Subject = "A new event was created"
            };

            try
            {
                await this._emailService.SendEmail(email);
            }
            catch (Exception ex)
            {
                //this shouldn't stop the API from doing else so this can be logged
                this._logger.LogError($"Mailing about event {@event.EventId} failed due to an error with the mail service: {ex.Message}");
            }

            return @event.EventId;
        }
    }
}