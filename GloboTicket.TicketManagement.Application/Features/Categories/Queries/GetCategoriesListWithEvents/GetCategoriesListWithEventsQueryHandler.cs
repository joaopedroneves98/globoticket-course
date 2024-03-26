namespace GloboTicket.TicketManagement.Application.Features.Categories.Queries.GetCategoriesListWithEvents
{
    using AutoMapper;

    using Contracts.Persistence;

    using MediatR;

    public class GetCategoriesListWithEventsQueryHandler : IRequestHandler<GetCategoriesListWithEventsQuery, List<CategoryEventListVm>>
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;

        public GetCategoriesListWithEventsQueryHandler(
            IMapper mapper, 
            ICategoryRepository categoryRepository)
        {
            this._mapper = mapper;
            this._categoryRepository = categoryRepository;
        }

        public async Task<List<CategoryEventListVm>> Handle(GetCategoriesListWithEventsQuery request, CancellationToken cancellationToken)
        {
            var list = await this._categoryRepository.GetCategoriesWithEvents(request.IncludeHistory);
            return this._mapper.Map<List<CategoryEventListVm>>(list);
        }
    }
}