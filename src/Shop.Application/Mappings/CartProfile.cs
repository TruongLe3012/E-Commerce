using AutoMapper;
using Shop.Application.DTOs.Cart;
using Shop.Domain.Entities;

namespace Shop.Application.Mappings
{
    public class CartProfile : Profile
    {
        public CartProfile()
        {
            CreateMap<CartItem, CartItemDto>()
                .ForMember(
                    dest => dest.ProductName,
                    opt => opt.MapFrom(
                        src => src.Product.Name));
        }
    }
}