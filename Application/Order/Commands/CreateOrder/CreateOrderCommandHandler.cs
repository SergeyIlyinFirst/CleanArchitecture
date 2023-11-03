using AutoMapper;
using DataAccess.Interfaces;
using Email.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebApp.Interfaces;
using OrderEntity = Entities.Models.Order;

namespace UseCases.Order.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, int>
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IBackgroundJobService _backgroundJobService;
        private readonly ICurrentUserService _currentUserService;

        public CreateOrderCommandHandler(IDbContext dbContext, IMapper mapper, IBackgroundJobService backgroundJobService, ICurrentUserService currentUserService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _backgroundJobService = backgroundJobService;
            _currentUserService = currentUserService;
        }

        public async Task<int> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            var order = _mapper.Map<OrderEntity>(command.Dto);
            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();

            _backgroundJobService.Schedule<IEmailService>(e => e.SendAsync(_currentUserService.Email, "OrderCreated", $"Order {order.Id} created"));

            return order.Id;
        }
    }
}
