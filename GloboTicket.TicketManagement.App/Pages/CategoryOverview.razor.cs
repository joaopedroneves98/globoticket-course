namespace GloboTicket.TicketManagement.App.Pages
{
    using Contracts;
    using ViewModels;
    using Microsoft.AspNetCore.Components;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    
    public partial class CategoryOverview
    {
        [Inject]
        public ICategoryDataService CategoryDataService{ get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public ICollection<CategoryEventsViewModel> Categories { get; set; }

        protected async override Task OnInitializedAsync()
        {
            this.Categories = await this.CategoryDataService.GetAllCategoriesWithEvents(false);
        }

        protected async void OnIncludeHistoryChanged(ChangeEventArgs args)
        {
            if((bool)args.Value)
            {
                this.Categories = await this.CategoryDataService.GetAllCategoriesWithEvents(true);
            }
            else
            {
                this.Categories = await this.CategoryDataService.GetAllCategoriesWithEvents(false);
            }
        }
    }
}
