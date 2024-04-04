namespace GloboTicket.TicketManagement.App.Pages
{
    using Contracts;
    using ViewModels;
    using Microsoft.AspNetCore.Components;

    public partial class Login
    {
        public LoginViewModel LoginViewModel { get; set; }
        
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public string Message { get; set; }

        [Inject]
        private IAuthenticationService AuthenticationService { get; set; }

        public Login()
        {
            
        }

        protected override void OnInitialized()
        {
            this.LoginViewModel = new LoginViewModel();
        }

        protected async void HandleValidSubmit()
        {
            if ((await this.AuthenticationService.Login(this.LoginViewModel.Email, this.LoginViewModel.Password)).Success)
            {
                this.NavigationManager.NavigateTo("/", true);
            }
            else
            {
                this.Message = "Username/password combination unknown";
            }
        }
    }
}
