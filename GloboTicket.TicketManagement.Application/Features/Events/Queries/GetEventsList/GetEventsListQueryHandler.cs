namespace GloboTicket.TicketManagement.Application.Features.Events.Queries.GetEventsList
{
    using AutoMapper;

    using GloboTicket.TicketManagement.Application.Contracts.Persistence;
    using GloboTicket.TicketManagement.Domain.Entities;

    using MediatR;

    public class GetEventsListQueryHandler : IRequestHandler<GetEventsListQuery, List<EventListVm>>
    {

        private readonly IAsyncRepository<Event> _eventRepository;
        private readonly IMapper _mapper;

        public GetEventsListQueryHandler(
            IMapper mapper, 
            IAsyncRepository<Event> eventRepository)
        {
            this._mapper = mapper;
            this._eventRepository = eventRepository;
        }

        public async Task<List<EventListVm>> Handle(GetEventsListQuery request, CancellationToken cancellationToken)
        {
            var allEvents = (await this._eventRepository.ListAllAsync()).OrderBy(x => x.Date);
            return this._mapper.Map<List<EventListVm>>(allEvents);
        }
    }
}