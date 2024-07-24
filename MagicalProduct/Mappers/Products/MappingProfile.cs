using AutoMapper;
using MagicalProduct.API.Models;
using MagicalProduct.API.Payload.Request.Products;

namespace MagicalProduct.API.Mappers.Products
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, GetRequest>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
            CreateMap<CreateRequest, Product>();
            CreateMap<Product, UpdateRequest>();
        }
    }
}
