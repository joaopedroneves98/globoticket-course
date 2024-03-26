namespace GloboTicket.TicketManagement.Application.Contracts.Infrastructure
{
    using Features.Events.Queries.GetEventsExport;

    using System.Collections.Generic;

    public interface ICsvExporter
    {
        byte[] ExportEventsToCsv(List<EventExportDto> eventExportDtos);
    }
}