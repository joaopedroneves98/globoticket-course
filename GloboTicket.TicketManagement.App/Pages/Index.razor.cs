namespace GloboTicket.TicketManagement.App.Pages
{
    using Auth;
    using Contracts;
    using Microsoft.AspNetCore.Components;
    using Microsoft.AspNetCore.Components.Authorization;

    public partial class Index
    {
        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IAuthenticationService AuthenticationService { get; set; }

        protected async override Task OnInitializedAsync()
        {
            await ((CookieAuthenticationStateProvider)this.AuthenticationStateProvider).GetAuthenticationStateAsync();
        }

        protected void NavigateToLogin()
        {
            this.NavigationManager.NavigateTo("login");
        }

        protected void NavigateToRegister()
        {
            this.NavigationManager.NavigateTo("register");
        }

        protected async void Logout()
        {
            await this.AuthenticationService.Logout();
            this.NavigationManager.NavigateTo("/", true);

        }
    }
}
