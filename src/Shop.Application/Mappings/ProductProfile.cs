using AutoMapper;
using Shop.Application.DTOs.Product;
using Shop.Domain.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Shop.Application.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>();

            CreateMap<CreateProductDto, Product>();

            CreateMap<UpdateProductDto, Product>();
        }
    }
}