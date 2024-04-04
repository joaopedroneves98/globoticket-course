namespace GloboTicket.TicketManagement.App.Services
{
    using Auth;
    using Contracts;
    using Base;

    public class AuthenticationService : IAuthenticationService
    {
        private readonly CookieAuthenticationStateProvider _cookieAuthenticationStateProvider;

        public AuthenticationService(CookieAuthenticationStateProvider cookieAuthenticationStateProvider)
        {
            this._cookieAuthenticationStateProvider = cookieAuthenticationStateProvider;
        }

        public async Task<ApiResponse> Login(string email, string password)
        {
            return await this._cookieAuthenticationStateProvider.Login(email, password);
        }

        public  async Task<ApiResponse> Register(string email, string password)
        {
            return await this._cookieAuthenticationStateProvider.Register(email, password);
        }

        public async Task Logout()
        {
            await this._cookieAuthenticationStateProvider.Logout();
        }
    }
}