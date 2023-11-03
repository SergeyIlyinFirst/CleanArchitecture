using AutoMapper;
using DataAccess.Interfaces;
using Delivery.Interfaces;
using DomainServices.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using UseCases.Order.DTOs;

namespace UseCases.Order.Queries.GetById
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderDto>
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IOrderDomainService _orderDomainService;
        private readonly IDeliveryService _deliveryService;

        public GetOrderByIdQueryHandler(IDbContext dbContext, IMapper mapper, IOrderDomainService orderDomainService, IDeliveryService deliveryService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _orderDomainService = orderDomainService;
            _deliveryService = deliveryService;
        }

        public async Task<OrderDto> Handle(GetOrderByIdQuery query, CancellationToken cancellationToken)
        {
            var order = await _dbContext.Orders
                .AsNoTracking()
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(x => x.Id == query.Id) 
                ?? throw new EntityNotFoundException();

            var dto = _mapper.Map<OrderDto>(order);
            dto.Total = await _orderDomainService.GetTotalAsync(order, _deliveryService.CalculateDeliveryCostAsync);

            return dto;
        }
    }
}
