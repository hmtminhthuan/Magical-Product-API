using AutoMapper;
using MagicalProduct.API.Enums;
using MagicalProduct.API.Middlewares;
using MagicalProduct.API.Models;
using MagicalProduct.API.Payload.Request.Categories;
using MagicalProduct.API.Payload.Response;
using MagicalProduct.Repo.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace MagicalProduct.API.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoriesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        //[AuthorizePolicy(RoleEnum.Admin, RoleEnum.Staff)]
        public IActionResult Search()
        {
            Expression<Func<Category, bool>> filter = null;


            var objects = _unitOfWork.CategoryRepository.Get();
            var requests = _mapper.Map<List<GetRequest>>(objects);

            var response = new BasicResponse
            {
                IsSuccess = true,
                Message = "Get list successfully",
                StatusCode = StatusCodes.Status200OK,
                Result = requests.ToArray()
            };
            return new ObjectResult(response) { StatusCode = StatusCodes.Status200OK };
        }
        [HttpGet("{id}")]
        //[AuthorizePolicy(RoleEnum.Admin)]
        public IActionResult GetById(int id)
        {
            // Include the Style property when retrieving the painting
            var category = _unitOfWork.CategoryRepository.Get(
                filter: p => p.Id == id
            ).FirstOrDefault();

            if (category == null)
            {
                throw new KeyNotFoundException("ID " + id + " does not exist");
            }

            // Map the painting to the response object, including StyleName
            var categoryDto = _mapper.Map<GetRequest>(category);

            var response = new BasicResponse
            {
                IsSuccess = true,
                Message = "Get by ID successfully",
                StatusCode = StatusCodes.Status200OK,
                Result = categoryDto
            };
            return new ObjectResult(response) { StatusCode = StatusCodes.Status200OK };
        }

        [HttpDelete("{id}")]
        //[AuthorizePolicy(RoleEnum.Admin)]
        public IActionResult Delete(int id)
        {
            var player = _unitOfWork.CategoryRepository.GetByID(id);
            if (player == null)
            {
                throw new KeyNotFoundException("ID " + id + " does not exist");
            }
            _unitOfWork.CategoryRepository.Delete(player);
            _unitOfWork.Save();

            var objects = _unitOfWork.CategoryRepository.Get();
            var requests = _mapper.Map<List<GetRequest>>(objects);

            var response = new BasicResponse
            {
                IsSuccess = true,
                Message = "Delete successfully",
                StatusCode = StatusCodes.Status200OK,
                Result = requests.ToArray()
            };
            return new ObjectResult(response) { StatusCode = StatusCodes.Status200OK };
        }

        
    }
}
