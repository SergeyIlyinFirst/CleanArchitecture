using System.Collections.Generic;

namespace UseCases.Order.DTOs
{
    public class CreateOrderDto
    {
        public List<OrderItemDto> Items { get; set; }
    }
}
