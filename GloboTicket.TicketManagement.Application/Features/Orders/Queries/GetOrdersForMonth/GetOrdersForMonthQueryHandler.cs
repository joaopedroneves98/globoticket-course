﻿namespace GloboTicket.TicketManagement.Application.Features.Orders.Queries.GetOrdersForMonth
{
    using AutoMapper;
    using Contracts.Persistence;
    using MediatR;

    public class GetOrdersForMonthQueryHandler : IRequestHandler<GetOrdersForMonthQuery, PagedOrdersForMonthVm>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public GetOrdersForMonthQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            this._orderRepository = orderRepository;
            this._mapper = mapper;
        }

        public async Task<PagedOrdersForMonthVm> Handle(GetOrdersForMonthQuery request, CancellationToken cancellationToken)
        {
            var list = await this._orderRepository.GetPagedOrdersForMonth(request.Date, request.Page, request.Size);
            var orders = this._mapper.Map<List<OrdersForMonthDto>>(list);

            var count = await this._orderRepository.GetTotalCountOfOrdersForMonth(request.Date);
            return new PagedOrdersForMonthVm() { Count = count, OrdersForMonth = orders, Page = request.Page, Size = request.Size };
        }
    }
}
