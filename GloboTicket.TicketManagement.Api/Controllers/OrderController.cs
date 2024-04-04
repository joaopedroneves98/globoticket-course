namespace GloboTicket.TicketManagement.Api.Controllers
{
    using Application.Features.Orders.Queries.GetOrdersForMonth;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet("/getpagedordersformonth", Name = "GetPagedOrdersForMonth")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<PagedOrdersForMonthVm>> GetPagedOrdersForMonth(DateTime date, int page, int size)
        {
            var getOrdersForMonthQuery = new GetOrdersForMonthQuery() { Date = date, Page = page, Size = size };
            var dtos = await this._mediator.Send(getOrdersForMonthQuery);

            return this.Ok(dtos);
        }
    }
}