namespace GloboTicket.TicketManagement.Application.Features.Categories.Commands.CreateCategory
{
    using FluentValidation;
    public class CreateCategoryCommandValidator: AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            this.RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 10 characters.");
        }
    }
}
