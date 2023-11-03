using AutoMapper;
using Entities.Models;
using UseCases.Order.DTOs;
using OrderEntity = Entities.Models.Order;

namespace UseCases.Order.Utils
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<OrderEntity, OrderDto>();
            CreateMap<CreateOrderDto, OrderEntity>();
            CreateMap<OrderItemDto, OrderItem>();
        }
    }
}
