namespace GloboTicket.TicketManagement.Api.Services
{
    using Application.Contracts;
    using System.Security.Claims;

    public class LoggedInUserService : ILoggedInUserService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public LoggedInUserService(IHttpContextAccessor httpContextAccessor)
        {
            this._contextAccessor = httpContextAccessor;
        }

        public string UserId
        {
            get
            {
                return this._contextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            }
        }
    }
}
