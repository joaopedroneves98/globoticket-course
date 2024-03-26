namespace GloboTicket.TicketManagement.Domain.Entities
{
    using Common;
    public class Category : AuditableEntity
    {
        public Guid CategoryId { get; set; }
        
        public string Name { get; set; } = string.Empty;
        
        public ICollection<Event>? Events { get; set; }
    }
}