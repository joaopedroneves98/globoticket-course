namespace GloboTicket.TicketManagement.Infrastructure.Mail
{
    using Application.Contracts.Infrastructure;
    using Application.Models.Mail;

    using Microsoft.Extensions.Options;

    using SendGrid;
    using SendGrid.Helpers.Mail;

    public class EmailService : IEmailService
    {
        public EmailSettings _emailSettings { get; }

        public EmailService(IOptions<EmailSettings> mailSettings)
        {
            this._emailSettings = mailSettings.Value;
        }

        public async Task<bool> SendEmail(Email email)
        {
            var client = new SendGridClient(this._emailSettings.ApiKey);

            var subject = email.Subject;
            var to = new EmailAddress(email.To);
            var emailBody = email.Body;

            var from = new EmailAddress
            {
                Email = this._emailSettings.FromAddress,
                Name = this._emailSettings.FromName
            };

            var sendGridMessage = MailHelper.CreateSingleEmail(from, to, subject, emailBody, emailBody);
            var response = await client.SendEmailAsync(sendGridMessage);

            if (response.StatusCode == System.Net.HttpStatusCode.Accepted || response.StatusCode == System.Net.HttpStatusCode.OK)
                return true;

            return false;
        }
    }
}