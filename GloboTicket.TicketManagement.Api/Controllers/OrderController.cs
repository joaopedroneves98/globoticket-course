namespace GloboTicket.TicketManagement.Api.Controllers
{
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
    }
}