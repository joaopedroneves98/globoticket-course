namespace GloboTicket.TicketManagement.Api.Utility
{
    [AttributeUsage(AttributeTargets.Method)]
    public class FileResultContentTypeAttribute : Attribute
    {
        public FileResultContentTypeAttribute(string contentType)
        {
            this.ContentType = contentType;
        }

        public string ContentType { get; }
    }
}