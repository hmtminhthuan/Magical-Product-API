using AutoMapper;
using MagicalProduct.API.Models;
using MagicalProduct.API.Payload.Request.Categories;

namespace MagicalProduct.API.Mappers.Categories
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, GetRequest>();         
            CreateMap<CreateRequest, Category>();
            CreateMap<Category, UpdateRequest>();
        }
    }
}
