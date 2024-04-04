namespace GloboTicket.TicketManagement.App.Services
{
    using AutoMapper;
    using Blazored.LocalStorage;
    using Contracts;
    using Base;
    using ViewModels;

    public class EventDataService: BaseDataService, IEventDataService
    {
        
        private readonly IMapper _mapper;

        public EventDataService(IClient client, IMapper mapper, ILocalStorageService localStorage) : base(client, localStorage)
        {
            this._mapper = mapper;
        }

        public async Task<List<EventListViewModel>> GetAllEvents()
        {
            var allEvents = await this._client.GetAllEventsAsync();
            var mappedEvents = this._mapper.Map<ICollection<EventListViewModel>>(allEvents);
            return mappedEvents.ToList();
        }

        public async Task<EventDetailViewModel> GetEventById(Guid id)
        {
            var selectedEvent = await this._client.GetEventByIdAsync(id);
            var mappedEvent = this._mapper.Map<EventDetailViewModel>(selectedEvent);
            return mappedEvent;
        }

        public async Task<ApiResponse<Guid>> CreateEvent(EventDetailViewModel eventDetailViewModel)
        {
            try
            {
                var createEventCommand = this._mapper.Map<CreateEventCommand>(eventDetailViewModel);
                var newId = await this._client.AddEventAsync(createEventCommand);
                return new ApiResponse<Guid>() { Data = newId, Success = true };
            }
            catch (ApiException ex)
            {
                return this.ConvertApiExceptions<Guid>(ex);
            }
        }

        public async Task<ApiResponse<Guid>> UpdateEvent(EventDetailViewModel eventDetailViewModel)
        {
            try
            {
                var updateEventCommand = this._mapper.Map<UpdateEventCommand>(eventDetailViewModel);
                await this._client.UpdateEventAsync(updateEventCommand);
                return new ApiResponse<Guid>() { Success = true };
            }
            catch (ApiException ex)
            {
                return this.ConvertApiExceptions<Guid>(ex);
            }
        }

        public async Task<ApiResponse<Guid>> DeleteEvent(Guid id)
        {
            try
            {
                await this._client.DeleteEventAsync(id);
                return new ApiResponse<Guid>() { Success = true };
            }
            catch (ApiException ex)
            {
                return this.ConvertApiExceptions<Guid>(ex);
            }
        }
    }
}
