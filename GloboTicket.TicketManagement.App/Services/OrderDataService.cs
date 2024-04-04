namespace GloboTicket.TicketManagement.App.Services
{
    using AutoMapper;
    using Blazored.LocalStorage;
    using Contracts;
    using Base;
    using ViewModels;

    public class OrderDataService : BaseDataService, IOrderDataService
    {
        private readonly IMapper _mapper;

        public OrderDataService(IClient client, IMapper mapper, ILocalStorageService localStorage) : base(client, localStorage)
        {
            this._mapper = mapper;
        }

        public async Task<PagedOrderForMonthViewModel> GetPagedOrderForMonth(DateTime date, int page, int size)
        {
            var orders = await this._client.GetPagedOrdersForMonthAsync(date, page, size);
            var mappedOrders = this._mapper.Map<PagedOrderForMonthViewModel>(orders);
            return mappedOrders;
        }
    }
}
