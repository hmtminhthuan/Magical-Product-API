using System.Linq.Expressions;
using AutoMapper;
using MagicalProduct.API.Payload.Response;
using MagicalProduct.API.Services;
using MagicalProduct.API.Services.Interfaces;
using MagicalProduct.API.Utils;
using MagicalProduct.Repo.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MagicalProduct.API.Payload.Request;
using MagicalProduct.API.Enums;

namespace MagicalProduct.API.Services.Implements
{
    public class UserService : BaseService<UserService>, IUserService
    {
        public UserService(IUnitOfWork unitOfWork, ILogger<UserService> logger, IMapper mapper,
            IHttpContextAccessor httpContextAccessor) : base(unitOfWork, logger, mapper, httpContextAccessor)
        {
        }

        public async Task<BasicResponse> Login(ProductRequest loginRequest)
        {
            var loginAccount = _unitOfWork.UserRepository
                .Get(filter: account => account.Email.Equals(loginRequest.Email)
                && account.Password.Equals(PasswordUtil.HashPassword(loginRequest.Password)),
                includeProperties: "Role").FirstOrDefault();
            if (loginAccount == null)
            {
                throw new KeyNotFoundException("Invalid username or password");
            }
            else if (loginAccount.Status == null || loginAccount.Status == false)
            {
                throw new BadHttpRequestException("Account is not active");
            }
            var token = JwtUtil.GenerateJwtToken(loginAccount);
            var response = new BasicResponse
            {
                IsSuccess = true,
                Message = "Login successfully",
                StatusCode = StatusCodes.Status200OK,
                Result = new LoginResponse
                {
                    AccessToken = token,
                    Id = loginAccount.Id,
                    Email = loginAccount.Email,
                    Gender = loginAccount.Gender,
                    Name = loginAccount.Name,
                    Phone = loginAccount.Phone,
                    RoleId = loginAccount.RoleId,
                    Status = loginAccount.Status
                }
            };
            return response;
        }
    }
}