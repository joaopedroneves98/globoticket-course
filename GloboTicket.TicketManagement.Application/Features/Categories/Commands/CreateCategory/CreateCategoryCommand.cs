namespace GloboTicket.TicketManagement.Application.Features.Categories.Commands.CreateCategory
{
    using MediatR;

    public class CreateCategoryCommand: IRequest<CreateCategoryCommandResponse>
    {
        public string Name { get; set; } = string.Empty;
    }
}
