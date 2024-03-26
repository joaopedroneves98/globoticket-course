namespace GloboTicket.TicketManagement.Api.Controllers
{
    using Api.Utility;
    using Application.Features.Events.Commands.CreateEvent;
    using Application.Features.Events.Commands.DeleteEvent;
    using Application.Features.Events.Commands.UpdateEvent;
    using Application.Features.Events.Queries.GetEventDetail;
    using Application.Features.Events.Queries.GetEventsExport;
    using Application.Features.Events.Queries.GetEventsList;

    using MediatR;

    using Microsoft.AspNetCore.Mvc;
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : Controller
    {
        private readonly IMediator _mediator;

        public EventsController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet(Name = "GetAllEvents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<EventListVm>>> GetAllEvents()
        {
            var result = await this._mediator.Send(new GetEventsListQuery());
            return this.Ok(result);
        }

        [HttpGet("{id}", Name = "GetEventById")]
        public async Task<ActionResult<EventDetailVm>> GetEventById(Guid id)
        {
            var getEventDetailQuery = new GetEventDetailQuery()
            {
                Id = id
            };
            return this.Ok(await this._mediator.Send(getEventDetailQuery));
        }

        [HttpPost(Name = "AddEvent")]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateEventCommand createEventCommand)
        {
            var id = await this._mediator.Send(createEventCommand);
            return this.Ok(id);
        }

        [HttpPut(Name = "UpdateEvent")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Update([FromBody] UpdateEventCommand updateEventCommand)
        {
            await this._mediator.Send(updateEventCommand);
            return this.NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteEvent")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(Guid id)
        {
            var deleteEventCommand = new DeleteEventCommand()
            {
                EventId = id
            };
            await this._mediator.Send(deleteEventCommand);
            return this.NoContent();
        }

        [HttpGet("export", Name = "ExportEvents")]
        [FileResultContentType("text/csv")]
        public async Task<FileResult> ExportEvents()
        {
            var fileDto = await this._mediator.Send(new GetEventsExportQuery());

            return this.File(fileDto.Data, fileDto.ContentType, fileDto.EventExportFileName);
        }
    }
}