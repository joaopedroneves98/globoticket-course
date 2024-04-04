namespace GloboTicket.TicketManagement.App.Profiles
{
    using AutoMapper;
    using Services;
    using ViewModels;

    public class Mappings : Profile
    {
        public Mappings()
        {
            //Vms are coming in from the API, ViewModel are the local entities in Blazor
            this.CreateMap<EventListVm, EventListViewModel>().ReverseMap();
            this.CreateMap<EventDetailVm, EventDetailViewModel>().ReverseMap();

            this.CreateMap<EventDetailViewModel, CreateEventCommand>().ReverseMap();
            this.CreateMap<EventDetailViewModel, UpdateEventCommand>().ReverseMap();

            this.CreateMap<CategoryEventDto, EventNestedViewModel>().ReverseMap();

            this.CreateMap<CategoryDto, CategoryViewModel>().ReverseMap();
            this.CreateMap<CategoryListVm, CategoryViewModel>().ReverseMap();
            this.CreateMap<CategoryEventListVm, CategoryEventsViewModel>().ReverseMap();
            this.CreateMap<CreateCategoryCommand, CategoryViewModel>().ReverseMap();
            this.CreateMap<CreateCategoryDto, CategoryDto>().ReverseMap();

            this.CreateMap<PagedOrdersForMonthVm, PagedOrderForMonthViewModel>().ReverseMap();
            this.CreateMap<OrdersForMonthDto, OrdersForMonthListViewModel>().ReverseMap();
        }
    }
}
