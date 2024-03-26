namespace GloboTicket.TicketManagement.Api.Controllers
{
    using Application.Features.Categories.Commands.CreateCategory;
    using Application.Features.Categories.Queries.GetCategoriesList;
    using Application.Features.Categories.Queries.GetCategoriesListWithEvents;

    using MediatR;

    using Microsoft.AspNetCore.Mvc;
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet("all", Name = "GetAllCategories")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<CategoryListVm>>> GetAllCategories()
        {
            var dtos = await this._mediator.Send(new GetCategoriesListQuery());
            return this.Ok(dtos);
        }

        [HttpGet("allwithevents", Name = "GetCategoriesWithEvents")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<CategoryEventListVm>>> GetCategoriesWithEvents(bool includeHistory)
        {
            var getCategoriesListWithEventsQuery = new GetCategoriesListWithEventsQuery()
            {
                IncludeHistory = includeHistory
            };

            var dtos = await this._mediator.Send(getCategoriesListWithEventsQuery);
            return this.Ok(dtos);
        }

        [HttpPost(Name = "AddCategory")]
        public async Task<ActionResult<CreateCategoryCommandResponse>> Create([FromBody] CreateCategoryCommand createCategoryCommand)
        {
            var response = await this._mediator.Send(createCategoryCommand);
            return this.Ok(response);
        }
    }
}