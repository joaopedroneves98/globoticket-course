namespace GloboTicket.TicketManagement.Application.Features.Events.Queries.GetEventDetail
{
    using AutoMapper;

    using Contracts.Persistence;
    using Domain.Entities;

    using MediatR;
    public class GetEventDetailQueryHandler : IRequestHandler<GetEventDetailQuery, EventDetailVm>
    {
        private readonly IAsyncRepository<Event> _eventRepository;
        private readonly IAsyncRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;

        public GetEventDetailQueryHandler(
            IMapper mapper,
            IAsyncRepository<Event> eventRepository,
            IAsyncRepository<Category> categoryRepository)
        {
            this._mapper = mapper;
            this._eventRepository = eventRepository;
            this._categoryRepository = categoryRepository;
        }

        public async Task<EventDetailVm> Handle(GetEventDetailQuery request, CancellationToken cancellationToken)
        {
            var @event = await this._eventRepository.GetByIdAsync(request.Id);
            var eventDetailDto = this._mapper.Map<EventDetailVm>(@event);

            var category = await this._categoryRepository.GetByIdAsync(@event.CategoryId);

            eventDetailDto.Category = this._mapper.Map<CategoryDto>(category);

            return eventDetailDto;
        }
    }
}
