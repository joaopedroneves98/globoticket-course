namespace GloboTicket.TicketManagement.App.Contracts
{
    using ViewModels;

    public interface IOrderDataService
    {
        Task<PagedOrderForMonthViewModel> GetPagedOrderForMonth(DateTime date, int page, int size);
    }
}
