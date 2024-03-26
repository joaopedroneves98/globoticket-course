namespace GloboTicket.TicketManagement.API.IntegrationTests.Controllers
{
    using Base;

    using Application.Features.Categories.Queries.GetCategoriesList;

    using System.Text.Json;

    public class CategoryControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;

        public CategoryControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            this._factory = factory;
        }

        [Fact]
        public async Task ReturnsSuccessResult()
        {
            var client = this._factory.GetAnonymousClient();

            var response = await client.GetAsync("/api/category/all");

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<List<CategoryListVm>>(responseString);

            Assert.IsType<List<CategoryListVm>>(result);
            Assert.NotEmpty(result);
        }
    }
}