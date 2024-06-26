﻿namespace GloboTicket.TicketManagement.App.Pages
{
    using Components;
    using Contracts;
    using ViewModels;
    using Microsoft.AspNetCore.Components;

    public partial class TicketSales
    {

        [Inject]
        public IOrderDataService OrderDataService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public string SelectedMonth { get; set; }
        public string SelectedYear { get; set; }

        public List<string> MonthList { get; set; } = new List<string>() { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12" };
        public List<string> YearList { get; set; } = new List<string>() { "2022", "2023", "2024" };

        private int? pageNumber = 1;

        private PaginatedList<OrdersForMonthListViewModel> paginatedList 
            = new PaginatedList<OrdersForMonthListViewModel>();

        private IEnumerable<OrdersForMonthListViewModel> ordersList;

        protected async Task GetSales()
        {
            DateTime dt = new DateTime(int.Parse(this.SelectedYear), int.Parse(this.SelectedMonth), 1);

            var orders = await this.OrderDataService.GetPagedOrderForMonth(dt, this.pageNumber.Value, 5);
            this.paginatedList = new PaginatedList<OrdersForMonthListViewModel>(orders.OrdersForMonth.ToList(), orders.Count, this.pageNumber.Value, 5);
            this.ordersList = this.paginatedList.Items;

            this.StateHasChanged();
        }

        public async void PageIndexChanged(int newPageNumber)
        {
            if (newPageNumber < 1 || newPageNumber > this.paginatedList.TotalPages)
            {
                return;
            }
            this.pageNumber = newPageNumber;
            await this.GetSales();
            this.StateHasChanged();
        }
    }
}