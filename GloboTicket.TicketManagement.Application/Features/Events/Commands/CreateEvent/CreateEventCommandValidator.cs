namespace GloboTicket.TicketManagement.Application.Features.Events.Commands.CreateEvent
{
    using FluentValidation;

    using Contracts.Persistence;

    using System;
    using System.Threading.Tasks;
    
    public class CreateEventCommandValidator : AbstractValidator<CreateEventCommand>
    {
        private readonly IEventRepository _eventRepository;
        
        public CreateEventCommandValidator(IEventRepository eventRepository)
        {
            this._eventRepository = eventRepository;

            this.RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            this.RuleFor(p => p.Date)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .GreaterThan(DateTime.Now);

            this.RuleFor(e => e)
                .MustAsync(this.EventNameAndDateUnique)
                .WithMessage("An event with the same name and date already exists.");

            this.RuleFor(p => p.Price)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .GreaterThan(0);
        }

        private async Task<bool> EventNameAndDateUnique(CreateEventCommand e, CancellationToken token)
        {
            return !await this._eventRepository.IsEventNameAndDateUnique(e.Name, e.Date);
        }
    }
}