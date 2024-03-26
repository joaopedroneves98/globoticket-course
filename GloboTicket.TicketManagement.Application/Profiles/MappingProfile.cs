namespace GloboTicket.TicketManagement.Application.Profiles
{
    using AutoMapper;

    using Domain.Entities;

    using Features.Categories.Commands.CreateCategory;
    using Features.Categories.Queries.GetCategoriesList;
    using Features.Categories.Queries.GetCategoriesListWithEvents;
    using Features.Events.Commands.CreateEvent;
    using Features.Events.Commands.UpdateEvent;
    using Features.Events.Queries.GetEventDetail;
    using Features.Events.Queries.GetEventsExport;
    using Features.Events.Queries.GetEventsList;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            this.CreateMap<Event, EventListVm>().ReverseMap();
            this.CreateMap<Event, EventDetailVm>().ReverseMap();
            this.CreateMap<Category, CategoryDto>().ReverseMap();
            this.CreateMap<Category, CategoryListVm>().ReverseMap();
            this.CreateMap<Category, CategoryEventListVm>().ReverseMap();
            this.CreateMap<Category, CreateCategoryCommand>().ReverseMap();
            this.CreateMap<Category, CreateCategoryDto>().ReverseMap();
            this.CreateMap<Event, CategoryEventDto>().ReverseMap();
            this.CreateMap<Event, EventExportDto>().ReverseMap();

            this.CreateMap<Event, CreateEventCommand>().ReverseMap();
            this.CreateMap<Event, UpdateEventCommand>().ReverseMap();
            this.CreateMap<Event, CategoryEventDto>().ReverseMap();
        }
    }
}