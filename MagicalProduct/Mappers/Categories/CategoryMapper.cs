using AutoMapper;
using MagicalProduct.API.Models;
using MagicalProduct.API.Payload.Request.Categories;

namespace MagicalProduct.API.Mappers.Categories
{
    public class CategoryMapper : Profile
    {
        public CategoryMapper()
        {
            CreateMap<Category, GetRequest>();         
            CreateMap<CreateCategoryRequest, Category>();
            CreateMap<Category, UpdateCategoryRequest>();
            CreateMap<UpdateCategoryRequest, Category>();
        }
    }
}
