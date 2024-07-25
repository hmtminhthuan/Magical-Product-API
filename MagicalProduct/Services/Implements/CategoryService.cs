using AutoMapper;
using MagicalProduct.API.Models;
using MagicalProduct.API.Payload.Request.Categories;
using MagicalProduct.API.Payload.Response;
using MagicalProduct.API.Services.Interfaces;
using MagicalProduct.Repo.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MagicalProduct.API.Services.Implements
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BasicResponse> SearchAsync()
        {
            var objects = _unitOfWork.CategoryRepository.Get();
            var requests = _mapper.Map<List<GetRequest>>(objects);

            var response = new BasicResponse
            {
                IsSuccess = true,
                Message = "Get list successfully",
                StatusCode = StatusCodes.Status200OK,
                Result = requests.ToArray()
            };
            return response;
        }

        public async Task<BasicResponse> GetByIdAsync(int id)
        {
            var category = _unitOfWork.CategoryRepository.Get(
                filter: p => p.Id == id
            ).FirstOrDefault();

            if (category == null)
            {
                throw new KeyNotFoundException("ID " + id + " does not exist");
            }

            var categoryDto = _mapper.Map<GetRequest>(category);

            var response = new BasicResponse
            {
                IsSuccess = true,
                Message = "Get by ID successfully",
                StatusCode = StatusCodes.Status200OK,
                Result = categoryDto
            };
            return response;
        }

        public async Task<BasicResponse> CreateAsync(CreateCategoryRequest request)
        {
            var category = _mapper.Map<Category>(request);

            var lastItem = _unitOfWork.CategoryRepository.Get(orderBy: item => item.OrderByDescending(item => item.Id))
                .FirstOrDefault();
            category.Id = lastItem == null ? 1 : lastItem.Id + 1;

            _unitOfWork.CategoryRepository.Insert(category);
            _unitOfWork.Save();

            var response = new BasicResponse
            {
                IsSuccess = true,
                Message = "Create successfully",
                StatusCode = StatusCodes.Status201Created,
                Result = category
            };
            return response;
        }

        public async Task<BasicResponse> UpdateAsync(int id, UpdateCategoryRequest request)
        {
            var category = _unitOfWork.CategoryRepository.GetByID(id);
            if (category == null)
            {
                throw new KeyNotFoundException("ID " + id + " does not exist");
            }

            _mapper.Map(request, category);
            _unitOfWork.CategoryRepository.Update(category);
            _unitOfWork.Save();

            var response = new BasicResponse
            {
                IsSuccess = true,
                Message = "Update successfully",
                StatusCode = StatusCodes.Status200OK,
                Result = category
            };
            return response;
        }

        public async Task<BasicResponse> DeleteAsync(int id)
        {
            var category = _unitOfWork.CategoryRepository.GetByID(id);
            if (category == null)
            {
                throw new KeyNotFoundException("ID " + id + " does not exist");
            }
            _unitOfWork.CategoryRepository.Delete(category);
            _unitOfWork.Save();

            var response = new BasicResponse
            {
                IsSuccess = true,
                Message = "Delete successfully",
                StatusCode = StatusCodes.Status200OK
            };
            return response;
        }
    }
}
