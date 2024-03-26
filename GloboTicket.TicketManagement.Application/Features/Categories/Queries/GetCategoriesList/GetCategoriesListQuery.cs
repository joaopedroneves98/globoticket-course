namespace GloboTicket.TicketManagement.Application.Features.Categories.Queries.GetCategoriesList
{
    using MediatR;

    public class GetCategoriesListQuery : IRequest<List<CategoryListVm>>
    {
    }
}
