namespace GloboTicket.TicketManagement.App.Pages
{
    using Contracts;
    using ViewModels;
    using Microsoft.AspNetCore.Components;
    using Microsoft.JSInterop;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;

    public partial class EventOverview
    {
        [Inject]
        public IEventDataService EventDataService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public ICollection<EventListViewModel> Events { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        protected async override Task OnInitializedAsync()
        {
            this.Events = await this.EventDataService.GetAllEvents();
        }

        protected void AddNewEvent()
        {
            this.NavigationManager.NavigateTo("/eventdetails");
        }

        [Inject]
        public HttpClient HttpClient { get; set; }

        protected async Task ExportEvents()
        {
            if (await this.JSRuntime.InvokeAsync<bool>("confirm", $"Do you want to export this list to Excel?"))
            {
                var response = await this.HttpClient.GetAsync($"https://localhost:7020/api/events/export");
                response.EnsureSuccessStatusCode();
                var fileBytes = await response.Content.ReadAsByteArrayAsync();
                var fileName = $"MyReport{DateTime.Now.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)}.csv";
                await this.JSRuntime.InvokeAsync<object>("saveAsFile", fileName, Convert.ToBase64String(fileBytes));
            }
        }
    }
}
