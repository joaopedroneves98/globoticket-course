namespace GloboTicket.TicketManagement.Application.Features.Categories.Queries.GetCategoriesListWithEvents
{
    using MediatR;

    public class GetCategoriesListWithEventsQuery : IRequest<List<CategoryEventListVm>>
    {
        public bool IncludeHistory { get; set; }
    }
}
