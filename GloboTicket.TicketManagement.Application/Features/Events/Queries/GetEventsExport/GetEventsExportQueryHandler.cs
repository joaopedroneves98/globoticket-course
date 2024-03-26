namespace GloboTicket.TicketManagement.Application.Features.Events.Queries.GetEventsExport
{
    using AutoMapper;

    using Contracts.Infrastructure;
    using Contracts.Persistence;

    using Domain.Entities;

    using MediatR;

    public class GetEventsExportQueryHandler : IRequestHandler<GetEventsExportQuery, EventExportFileVm>
    {
        private readonly IAsyncRepository<Event> _eventRepository;
        private readonly IMapper _mapper;
        private readonly ICsvExporter _csvExporter;

        public GetEventsExportQueryHandler(IMapper mapper, IAsyncRepository<Event> eventRepository, ICsvExporter csvExporter)
        {
            this._mapper = mapper;
            this._eventRepository = eventRepository;
            this._csvExporter = csvExporter;
        }

        public async Task<EventExportFileVm> Handle(GetEventsExportQuery request, CancellationToken cancellationToken)
        {
            var allEvents = this._mapper.Map<List<EventExportDto>>((await this._eventRepository.ListAllAsync()).OrderBy(x => x.Date));

            var fileData = this._csvExporter.ExportEventsToCsv(allEvents);

            var eventExportFileDto = new EventExportFileVm()
            {
                ContentType = "text/csv",
                Data = fileData,
                EventExportFileName = $"{Guid.NewGuid()}.csv"
            };

            return eventExportFileDto;
        }
    }
}