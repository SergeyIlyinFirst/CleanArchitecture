using MediatR;
using UseCases.Order.DTOs;

namespace UseCases.Order.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest<int>
    {
        public CreateOrderDto Dto { get; set; }
    }
}
