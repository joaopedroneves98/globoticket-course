namespace GloboTicket.TicketManagement.Application.Features.Categories.Queries.GetCategoriesList
{
    using AutoMapper;

    using Contracts.Persistence;
    using Domain.Entities;

    using MediatR;

    public class GetCategoriesListQueryHandler : IRequestHandler<GetCategoriesListQuery, List<CategoryListVm>>
    {
        private readonly IAsyncRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;

        public GetCategoriesListQueryHandler(
            IMapper mapper, 
            IAsyncRepository<Category> categoryRepository)
        {
            this._mapper = mapper;
            this._categoryRepository = categoryRepository;
        }

        public async Task<List<CategoryListVm>> Handle(GetCategoriesListQuery request, CancellationToken cancellationToken)
        {
            var allCategories = (await this._categoryRepository.ListAllAsync()).OrderBy(x => x.Name);
            return this._mapper.Map<List<CategoryListVm>>(allCategories);
        }
    }
}