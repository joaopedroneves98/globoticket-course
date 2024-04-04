namespace GloboTicket.TicketManagement.App.Services
{
    using AutoMapper;
    using Blazored.LocalStorage;
    using Contracts;
    using Base;
    using ViewModels;

    public class CategoryDataService : BaseDataService, ICategoryDataService
    {
        private readonly IMapper _mapper;

        public CategoryDataService(IClient client, IMapper mapper, ILocalStorageService localStorage): base(client, localStorage)
        {
            this._mapper = mapper;
        }

        public async Task<List<CategoryViewModel>> GetAllCategories()
        {
            var allCategories = await this._client.GetAllCategoriesAsync();
            var mappedCategories = this._mapper.Map<ICollection<CategoryViewModel>>(allCategories);
            return mappedCategories.ToList();

        }

        public async Task<List<CategoryEventsViewModel>> GetAllCategoriesWithEvents(bool includeHistory)
        {
            var allCategories = await this._client.GetCategoriesWithEventsAsync(includeHistory);
            var mappedCategories = this._mapper.Map<ICollection<CategoryEventsViewModel>>(allCategories);
            return mappedCategories.ToList();
        }

        public async Task<ApiResponse<CategoryDto>> CreateCategory(CategoryViewModel categoryViewModel)
        {
            try
            {
                var apiResponse = new ApiResponse<CategoryDto>();
                var createCategoryCommand = this._mapper.Map<CreateCategoryCommand>(categoryViewModel);
                var createCategoryCommandResponse = await this._client.AddCategoryAsync(createCategoryCommand);
                if (createCategoryCommandResponse.Success)
                {
                    apiResponse.Data = this._mapper.Map<CategoryDto>(createCategoryCommandResponse.Category);
                    apiResponse.Success = true;
                }
                else
                {
                    apiResponse.Data = null;
                    foreach (var error in createCategoryCommandResponse.ValidationErrors)
                    {
                        apiResponse.ValidationErrors += error + Environment.NewLine;
                    }
                }
                return apiResponse;
            }
            catch (ApiException ex)
            {
                return this.ConvertApiExceptions<CategoryDto>(ex);
            }
        }
    }
}