using AutoMapper;
using Firebase.Storage;
using MagicalProduct.API.Enums;
using MagicalProduct.API.Models;
using MagicalProduct.API.Payload.Request.Products;
using MagicalProduct.API.Payload.Response;
using MagicalProduct.API.Services.Interfaces;
using MagicalProduct.API.Utils;
using MagicalProduct.Repo.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MagicalProduct.API.Services.Implements
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductService> _logger;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ProductService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<BasicResponse> SearchAsync(GetProductRequest getProductRequest)
        {
            if (getProductRequest.CategoryId != null)
            {
                var category = _unitOfWork.CategoryRepository.GetByID(getProductRequest.CategoryId.Value);
                if (category == null)
                {
                    throw new KeyNotFoundException("Category ID " + getProductRequest.CategoryId + " does not exist");
                }
            }

            var products = _unitOfWork.ProductRepository.Get(filter: p =>
                (string.IsNullOrEmpty(getProductRequest.SearchName) || p.Name.ToLower().Contains(getProductRequest.SearchName.Trim().ToLower()))
                && (!getProductRequest.CategoryId.HasValue || p.CategoryId == getProductRequest.CategoryId)
                && (!getProductRequest.MinPrice.HasValue || p.Price >= getProductRequest.MinPrice)
                && (!getProductRequest.MaxPrice.HasValue || p.Price <= getProductRequest.MaxPrice)
                && (!getProductRequest.ProductStatus.HasValue || p.Status == getProductRequest.ProductStatus),
                orderBy: !string.IsNullOrEmpty(getProductRequest.OrderFields) ?
                    (Func<IQueryable<Product>, IOrderedQueryable<Product>>)(query => query.OrderByMultipleFields(ParseOrderByFields(getProductRequest.OrderFields))) :
                    null
            );

            int pageIndex = getProductRequest.PageIndex ?? 1;
            int pageSize = getProductRequest.PageSize ?? 50;
            var totalItems = products.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            var response = new BasicResponse
            {
                IsSuccess = true,
                Message = "Get product list successfully",
                StatusCode = StatusCodes.Status200OK,
                Result = new GetProductResponse
                {
                    TotalItems = totalItems,
                    TotalPages = totalPages,
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    First = totalPages == 0 ? true : (pageIndex <= 0 || pageIndex > totalPages ? null : pageIndex == 1),
                    Last = totalPages == 0 ? true : (pageIndex > totalPages ? null : pageIndex == totalPages),
                    Data = products.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList()
                }
            };
            return response;
        }

        public async Task<BasicResponse> GetByIdAsync(string id)
        {
            var product = _unitOfWork.ProductRepository.GetByID(id);
            if (product == null)
            {
                throw new KeyNotFoundException("Product ID " + id + " does not exist");
            }

            var categoryOfProduct = _unitOfWork.CategoryRepository.GetByID(product.CategoryId);

            var response = new BasicResponse
            {
                IsSuccess = true,
                Message = "Get product by ID successfully",
                StatusCode = StatusCodes.Status200OK,
                Result = new
                {
                    product.Id,
                    product.Name,
                    product.Description,
                    product.Price,
                    product.ImageUrl,
                    product.CategoryId,
                    CategoryName = categoryOfProduct?.Name,
                    product.Status
                }
            };
            return response;
        }

        public async Task<BasicResponse> CreateAsync(CreateRequest createReq)
        {
            try
            {
                var categoryExist = _unitOfWork.CategoryRepository.GetByID(createReq.CategoryId);
                if (categoryExist == null)
                {
                    return new BasicResponse
                    {
                        IsSuccess = false,
                        Message = "Category ID " + createReq.CategoryId + " does not exist",
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                }

                var newProduct = new Product
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = createReq.Name,
                    Description = createReq.Description,
                    Price = createReq.Price,
                    CategoryId = createReq.CategoryId,
                    Status = createReq.Status,
                };

                if (createReq.ImageFile != null && createReq.ImageFile.Length > 0)
                {
                    var imageFileName = Guid.NewGuid().ToString();
                    newProduct.ImageUrl = await UploadImageToFirebase(createReq.ImageFile, imageFileName);
                }

                _unitOfWork.ProductRepository.Insert(newProduct);
                await _unitOfWork.SaveAsync();

                var response = new BasicResponse
                {
                    IsSuccess = true,
                    Message = "Create new product successfully",
                    StatusCode = StatusCodes.Status201Created,
                    Result = newProduct
                };
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a new product");
                return new BasicResponse
                {
                    IsSuccess = false,
                    Message = "An error occurred while creating a new product: " + ex.Message,
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

        public async Task<BasicResponse> UpdateAsync(UpdateRequest updateReq)
        {
            var updatedProduct = _unitOfWork.ProductRepository.GetByID(updateReq.Id);
            if (updatedProduct == null)
            {
                return new BasicResponse
                {
                    IsSuccess = false,
                    Message = "Product ID " + updateReq.Id + " does not exist",
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }

            if (updateReq.CategoryId.HasValue)
            {
                var categoryExist = _unitOfWork.CategoryRepository.GetByID(updateReq.CategoryId.Value);
                if (categoryExist == null)
                {
                    return new BasicResponse
                    {
                        IsSuccess = false,
                        Message = "Category ID " + updateReq.CategoryId.Value + " does not exist",
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                }
                updatedProduct.CategoryId = updateReq.CategoryId.Value;
            }
            if (!string.IsNullOrWhiteSpace(updateReq.Name)) updatedProduct.Name = updateReq.Name;
            if (updateReq.Price.HasValue) updatedProduct.Price = updateReq.Price.Value;
            if (!string.IsNullOrWhiteSpace(updateReq.Description)) updatedProduct.Description = updateReq.Description;
            if (updateReq.Status.HasValue) updatedProduct.Status = updateReq.Status.Value;

            if (updateReq.ImageFile != null && updateReq.ImageFile.Length > 0)
            {
                var imageFileName = Guid.NewGuid().ToString();
                updatedProduct.ImageUrl = await UploadImageToFirebase(updateReq.ImageFile, imageFileName);
            }

            _unitOfWork.ProductRepository.Update(updatedProduct);
            await _unitOfWork.SaveAsync();

            var response = new BasicResponse
            {
                IsSuccess = true,
                Message = "Update product successfully",
                StatusCode = StatusCodes.Status200OK,
                Result = updatedProduct
            };
            return response;
        }

        public async Task<BasicResponse> DeleteAsync(string id)
        {
            var productExist = _unitOfWork.ProductRepository.GetByID(id);
            if (productExist == null)
            {
                return new BasicResponse
                {
                    IsSuccess = false,
                    Message = "Product ID " + id + " does not exist",
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }

            productExist.Status = false; // Deactivate the product
            await _unitOfWork.SaveAsync();

            var response = new BasicResponse
            {
                IsSuccess = true,
                Message = "Deactivate product successfully",
                StatusCode = StatusCodes.Status200OK
            };
            return response;
        }

        private List<(string field, bool isDescending)> ParseOrderByFields(string orderByFields)
        {
            var fields = orderByFields.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            var orderFieldsList = new List<(string field, bool isDescending)>();

            foreach (var field in fields)
            {
                var parts = field.Split(':');
                var fieldName = CheckProductSortedField(parts[0]);
                var isDescending = parts.Length > 1 && parts[1].ToLower() == "desc";
                orderFieldsList.Add((fieldName, isDescending));
            }

            return orderFieldsList;
        }

        private string CheckProductSortedField(string fieldCode)
        {
            if (string.IsNullOrEmpty(fieldCode) || !int.TryParse(fieldCode, out int fieldNumber))
            {
                throw new BadHttpRequestException("Invalid sorted field. The code number is invalid");
            }
            switch (fieldNumber)
            {
                case (int)ProductSortField.ProductId:
                    return nameof(Product.Id);
                case (int)ProductSortField.ProductName:
                    return nameof(Product.Name);
                case (int)ProductSortField.CategoryId:
                    return nameof(Product.CategoryId);
                case (int)ProductSortField.Price:
                    return nameof(Product.Price);
                case (int)ProductSortField.Status:
                    return nameof(Product.Status);
                default:
                    throw new BadHttpRequestException("Invalid sorted field. Not exist the code number " + fieldNumber);
            }
        }

        private async Task<string> UploadImageToFirebase(IFormFile imageFile, string fileName)
        {
            try
            {
                var firebase = new FirebaseStorage("prn231-ea891.appspot.com")
                    .Child("images")
                    .Child(fileName);

                using (var stream = imageFile.OpenReadStream())
                {
                    var uploadTask = await firebase.PutAsync(stream);
                    return uploadTask;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to upload file: " + ex.Message);
            }
        }

        private async Task DeleteImageFromFirebase(string imageUrl)
        {
            try
            {
                var firebase = new FirebaseStorage("prn231-ea891.appspot.com")
                    .Child("images")
                    .Child(imageUrl);
                await firebase.DeleteAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to delete file: " + ex.Message);
            }
        }
    }
}
