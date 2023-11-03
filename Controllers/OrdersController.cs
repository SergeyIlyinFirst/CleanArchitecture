using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UseCases.Order.Commands.CreateOrder;
using UseCases.Order.DTOs;
using UseCases.Order.Queries.GetById;

namespace Mobile.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly ISender _sender;

        public OrdersController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("{id}")]
        public async Task<OrderDto> Get(int id)
        {
            var result = await _sender.Send(new GetOrderByIdQuery { Id = id });
            return result;
        }

        [HttpPost]
        public async Task<int> Post([FromBody] CreateOrderDto createOrderDto)
        {
            var result = await _sender.Send(new CreateOrderCommand { Dto = createOrderDto });
            return result;
        }
    }
}