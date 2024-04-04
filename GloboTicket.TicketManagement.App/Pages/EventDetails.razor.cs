namespace GloboTicket.TicketManagement.App.Pages
{
    using Contracts;
    using Services.Base;
    using ViewModels;
    using Microsoft.AspNetCore.Components;
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;

    public partial class EventDetails
    {
        [Inject]
        public IEventDataService EventDataService { get; set; }

        [Inject]
        public ICategoryDataService CategoryDataService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public EventDetailViewModel EventDetailViewModel { get; set; } 
            = new EventDetailViewModel() { Date = DateTime.Now.AddDays(1) };

        public ObservableCollection<CategoryViewModel> Categories { get; set; } 
            = new ObservableCollection<CategoryViewModel>();

        public string Message { get; set; }
        public string SelectedCategoryId { get; set; }

        [Parameter]
        public string EventId { get; set; }
        private Guid SelectedEventId = Guid.Empty;

        protected override async Task OnInitializedAsync()
        {
            if (Guid.TryParse(this.EventId, out this.SelectedEventId))
            {
                this.EventDetailViewModel = await this.EventDataService.GetEventById(this.SelectedEventId);
                this.SelectedCategoryId = this.EventDetailViewModel.CategoryId.ToString();
            }

            var list = await this.CategoryDataService.GetAllCategories();
            this.Categories = new ObservableCollection<CategoryViewModel>(list);
            this.SelectedCategoryId = this.Categories.FirstOrDefault().CategoryId.ToString();
        }

        protected async Task HandleValidSubmit()
        {
            this.EventDetailViewModel.CategoryId = Guid.Parse(this.SelectedCategoryId);
            ApiResponse<Guid> response;

            if (this.SelectedEventId == Guid.Empty)
            {
                response = await this.EventDataService.CreateEvent(this.EventDetailViewModel);
            }
            else
            {
                 response = await this.EventDataService.UpdateEvent(this.EventDetailViewModel);
            }
            this.HandleResponse(response);

        }

        protected async Task DeleteEvent()
        {
            var response = await this.EventDataService.DeleteEvent(this.SelectedEventId);
            this.HandleResponse(response);
        }

        protected void NavigateToOverview()
        {
            this.NavigationManager.NavigateTo("/eventoverview");
        }

        private void HandleResponse(ApiResponse<Guid> response)
        {
            if (response.Success)
            {
                this.NavigationManager.NavigateTo("/eventoverview");
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
