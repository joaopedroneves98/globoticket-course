namespace GloboTicket.TicketManagement.Application.Features.Categories.Commands.CreateCategory
{
    using Responses;
    public class CreateCategoryCommandResponse: BaseResponse
    {
        public CreateCategoryCommandResponse(): base()
        {

        }

        public CreateCategoryDto Category { get; set; } = default!;
    }
}