using AutoMapper;
using MagicalProduct.API.Enums;
using MagicalProduct.API.Middlewares;
using MagicalProduct.API.Models;
using MagicalProduct.API.Payload.Request.Products;
using MagicalProduct.API.Payload.Response;
using MagicalProduct.Repo.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq.Expressions;

namespace MagicalProduct.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        //[AuthorizePolicy(RoleEnum.Admin, RoleEnum.Staff)]
        public IActionResult Search(string? name, string? description, decimal? price = null, int pageSize = 10, int pageIndex = 1)
        {
            Expression<Func<Product, bool>> filter = null;

            if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(description) && price.HasValue)
            {
                filter = p => p.Name.Contains(name) || p.Description.Contains(description) || p.Price == price.Value;
            }
            else if (!string.IsNullOrWhiteSpace(name))
            {
                filter = p => p.Name.Contains(name);
            }
            else if (!string.IsNullOrWhiteSpace(description))
            {
                filter = p => p.Description.Contains(description);
            }
            else if (price.HasValue)
            {
                filter = p => p.Price == price.Value;
            }

            var objects = _unitOfWork.ProductRepository.Get(
                            filter: filter,
                            includeProperties: "Category",
                            pageIndex: pageIndex,
                            pageSize: pageSize
                            );
            var requests = _mapper.Map<List<GetRequest>>(objects);
            int totalItems = objects.Count();

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
        public IActionResult GetById(string id)
        {
            // Include the Style property when retrieving the painting
            var product = _unitOfWork.ProductRepository.Get(
                filter: p => p.Id == id,
                includeProperties: "Category"
            ).FirstOrDefault();

            if (product == null)
            {
                throw new KeyNotFoundException("ID " + id + " does not exist");
            }

            // Map the painting to the response object, including StyleName
            var productDto = _mapper.Map<GetRequest>(product);

            var response = new BasicResponse
            {
                IsSuccess = true,
                Message = "Get by ID successfully",
                StatusCode = StatusCodes.Status200OK,
                Result = productDto
            };
            return new ObjectResult(response) { StatusCode = StatusCodes.Status200OK };
        }

        [HttpPost]
        //[AuthorizePolicy(RoleEnum.Admin)]
        public async Task<IActionResult> CreateNew(CreateRequest createReq)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Guid id = Guid.NewGuid();
            var newProduct = _mapper.Map<Product>(createReq);
            newProduct.Id = id.ToString();
            newProduct.Category = _unitOfWork.CategoryRepository.GetByID(createReq.CategoryId);

            _unitOfWork.ProductRepository.Insert(newProduct);
            await _unitOfWork.SaveAsync();

            var response = new BasicResponse
            {
                IsSuccess = true,
                Message = "Create new successfully",
                StatusCode = StatusCodes.Status201Created,
                Result = _mapper.Map<UpdateRequest>(newProduct)
            };
            return new ObjectResult(response) { StatusCode = StatusCodes.Status201Created };
        }

        [HttpDelete("{id}")]
        //[AuthorizePolicy(RoleEnum.Admin)]
        public IActionResult Delete(string id)
        {
            var player = _unitOfWork.ProductRepository.GetByID(id);
            if (player == null)
            {
                throw new KeyNotFoundException("ID " + id + " does not exist");
            }
            _unitOfWork.ProductRepository.Delete(player);
            _unitOfWork.Save();
            
            var objects = _unitOfWork.ProductRepository.Get(includeProperties: "Category");
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

        [HttpPut]
        //[AuthorizePolicy(RoleEnum.Admin)]
        public IActionResult Update(UpdateRequest updateReq)
        {
            var updatedProduct = _unitOfWork.ProductRepository.GetByID(updateReq.Id);
            if (updatedProduct == null)
            {
                throw new KeyNotFoundException("ID does not exist");
            }
            if (updateReq.Id != updatedProduct.Id)
            {
                updatedProduct.Category = _unitOfWork.CategoryRepository.GetByID(updateReq.CategoryId);
            }

            var requests = _mapper.Map(updateReq, updatedProduct);
            _unitOfWork.ProductRepository.Update(requests);
            _unitOfWork.Save();

            var response = new BasicResponse
            {
                IsSuccess = true,
                Message = "Update successfully",
                StatusCode = StatusCodes.Status200OK,
                Result = _mapper.Map<UpdateRequest>(updatedProduct)
            };
            return new ObjectResult(response) { StatusCode = StatusCodes.Status200OK };
        }
    }
}
