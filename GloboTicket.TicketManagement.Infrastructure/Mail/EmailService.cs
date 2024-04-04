namespace GloboTicket.TicketManagement.Infrastructure.Mail
{
    using Application.Contracts.Infrastructure;
    using Application.Models.Mail;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using SendGrid;
    using SendGrid.Helpers.Mail;

    public class EmailService : IEmailService
    {
        private EmailSettings _emailSettings { get; }
        private ILogger<EmailService> _logger { get; }

        public EmailService(IOptions<EmailSettings> mailSettings, ILogger<EmailService> logger)
        {
            this._emailSettings = mailSettings.Value;
            this._logger = logger;
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

            this._logger.LogInformation("Email sent");

            if (response.StatusCode is System.Net.HttpStatusCode.Accepted or System.Net.HttpStatusCode.OK)
                return true;

            this._logger.LogError("Email sending failed");

            return false;
        }
    }
}