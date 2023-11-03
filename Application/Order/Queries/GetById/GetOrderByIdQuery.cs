using MediatR;
using UseCases.Order.DTOs;

namespace UseCases.Order.Queries.GetById
{
    public class GetOrderByIdQuery : IRequest<OrderDto>
    {
        public int Id { get; set; }
    }
}
