namespace GloboTicket.TicketManagement.App.Contracts
{
    using Services.Base;

    public interface IAuthenticationService
    {
        Task<ApiResponse> Login(string email, string password);
        Task<ApiResponse> Register(string email, string password);
        Task Logout();
    }
}
