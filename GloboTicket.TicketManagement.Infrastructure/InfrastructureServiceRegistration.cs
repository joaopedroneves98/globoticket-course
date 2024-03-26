namespace GloboTicket.TicketManagement.Infrastructure
{
    using Application.Contracts.Infrastructure;
    using Application.Models.Mail;

    using FileExport;

    using Mail;

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));

            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<ICsvExporter, CsvExporter>();

            return services;
        }
    }
}