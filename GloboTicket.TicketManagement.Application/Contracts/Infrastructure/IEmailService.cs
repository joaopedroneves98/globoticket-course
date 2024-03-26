namespace GloboTicket.TicketManagement.Application.Contracts.Infrastructure
{
    using Models.Mail;

    public interface IEmailService
    {
        Task<bool> SendEmail(Email email);
    }
}