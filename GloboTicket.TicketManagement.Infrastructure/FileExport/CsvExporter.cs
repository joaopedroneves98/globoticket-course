namespace GloboTicket.TicketManagement.Infrastructure.FileExport
{
    using CsvHelper;

    using Application.Contracts.Infrastructure;
    using Application.Features.Events.Queries.GetEventsExport;

    public class CsvExporter : ICsvExporter
    {
        public byte[] ExportEventsToCsv(List<EventExportDto> eventExportDtos)
        {
            using var memoryStream = new MemoryStream();
            using (var streamWriter = new StreamWriter(memoryStream))
            {
                using var csvWriter = new CsvWriter(streamWriter);
                csvWriter.WriteRecords(eventExportDtos);
            }

            return memoryStream.ToArray();
        }
    }
}