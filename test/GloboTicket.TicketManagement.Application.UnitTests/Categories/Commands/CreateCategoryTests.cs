namespace GloboTicket.TicketManagement.Application.UnitTests.Categories.Commands
{
    using AutoMapper;

    using Contracts.Persistence;

    using Profiles;

    using Mocks;

    using Domain.Entities;

    using Features.Categories.Commands.CreateCategory;

    using Moq;

    using Shouldly;

    public class CreateCategoryTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IAsyncRepository<Category>> _mockCategoryRepository;

        public CreateCategoryTests()
        {
            this._mockCategoryRepository = RepositoryMocks.GetCategoryRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            this._mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task Handle_ValidCategory_AddedToCategoriesRepo()
        {
            var handler = new CreateCategoryCommandHandler(this._mapper, this._mockCategoryRepository.Object);

            await handler.Handle(new CreateCategoryCommand()
            {
                Name = "Test"
            }, CancellationToken.None);

            var allCategories = await this._mockCategoryRepository.Object.ListAllAsync();
            allCategories.Count.ShouldBe(5);
        }
    }
}