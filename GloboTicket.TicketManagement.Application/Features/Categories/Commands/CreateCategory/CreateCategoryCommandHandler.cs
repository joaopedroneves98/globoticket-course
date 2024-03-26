namespace GloboTicket.TicketManagement.Application.Features.Categories.Commands.CreateCategory
{
    using AutoMapper;

    using Contracts.Persistence;
    using Domain.Entities;

    using MediatR;
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CreateCategoryCommandResponse>
    {
        private readonly IAsyncRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;

        public CreateCategoryCommandHandler(
            IMapper mapper, 
            IAsyncRepository<Category> categoryRepository)
        {
            this._mapper = mapper;
            this._categoryRepository = categoryRepository;
        }

        public async Task<CreateCategoryCommandResponse> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var createCategoryCommandResponse = new CreateCategoryCommandResponse();

            var validator = new CreateCategoryCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
            {
                createCategoryCommandResponse.Success = false;
                createCategoryCommandResponse.ValidationErrors = new List<string>();
                foreach (var error in validationResult.Errors)
                {
                    createCategoryCommandResponse.ValidationErrors.Add(error.ErrorMessage);
                }
            }
            if (createCategoryCommandResponse.Success)
            {
                var category = new Category() { Name = request.Name };
                category = await this._categoryRepository.AddAsync(category);
                createCategoryCommandResponse.Category = this._mapper.Map<CreateCategoryDto>(category);
            }

            return createCategoryCommandResponse;
        }
    }
}
