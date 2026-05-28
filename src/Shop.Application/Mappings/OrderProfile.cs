using AutoMapper;
using Shop.Application.DTOs.Order;
using Shop.Domain.Entities;

namespace Shop.Application.Mappings
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(
                    dest => dest.ProductName,
                    opt => opt.MapFrom(
                        src => src.Product.Name));

            CreateMap<Order, OrderDto>()
                .ForMember(
                    dest => dest.Items,
                    opt => opt.MapFrom(
                        src => src.OrderItems));
        }
    }
}