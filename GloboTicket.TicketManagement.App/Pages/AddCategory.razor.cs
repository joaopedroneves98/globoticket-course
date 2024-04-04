namespace GloboTicket.TicketManagement.App.Pages
{
    using Contracts;
    using Services;
    using Services.Base;
    using ViewModels;
    using Microsoft.AspNetCore.Components;
    using System.Threading.Tasks;

    public partial class AddCategory
    {
        [Inject]
        public ICategoryDataService CategoryDataService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public CategoryViewModel CategoryViewModel { get; set; }
        public string Message { get; set; }

        protected override void OnInitialized()
        {
            this.CategoryViewModel = new CategoryViewModel();
        }

        protected async Task HandleValidSubmit()
        {
            var response = await this.CategoryDataService.CreateCategory(this.CategoryViewModel);
            this.HandleResponse(response);
        }

        private void HandleResponse(ApiResponse<CategoryDto> response)
        {
            if (response.Success)
            {
                this.Message = "Category added";
            }
            else
            {
                this.Message = response.Message;
                if (!string.IsNullOrEmpty(response.ValidationErrors))
                    this.Message += response.ValidationErrors;
            }
        }
    }
}
