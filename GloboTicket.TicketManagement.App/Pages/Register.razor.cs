namespace GloboTicket.TicketManagement.App.Pages
{
    using Contracts;
    using ViewModels;
    using Microsoft.AspNetCore.Components;

    public partial class Register
    {

        public RegisterViewModel RegisterViewModel { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public string Message { get; set; }

        [Inject]
        private IAuthenticationService AuthenticationService { get; set; }

        public Register()
        {

        }
        protected override void OnInitialized()
        {
            this.RegisterViewModel = new RegisterViewModel();
        }

        protected async void HandleValidSubmit()
        {
            await this.AuthenticationService.Register(this.RegisterViewModel.Email, this.RegisterViewModel.Password);

            this.NavigationManager.NavigateTo("home");
        }
    }
}
