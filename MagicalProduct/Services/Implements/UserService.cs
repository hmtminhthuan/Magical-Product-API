using AutoMapper;
using MagicalProduct.API.Payload.Response;
using MagicalProduct.API.Services.Interfaces;
using MagicalProduct.API.Utils;
using MagicalProduct.Repo.Interfaces;
using MagicalProduct.API.Payload.Request;
using MagicalProduct.API.Models;

namespace MagicalProduct.API.Services.Implements
{
    public class UserService : BaseService<UserService>, IUserService
    {
        public UserService(IUnitOfWork unitOfWork, ILogger<UserService> logger) : base(unitOfWork, logger)
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

        public async Task<BasicResponse> GetAllUsersAsync()
        {
            var users = _unitOfWork.UserRepository.Get(includeProperties: "Role");
            var response = new BasicResponse
            {
                IsSuccess = true,
                Message = "Get all users successfully",
                StatusCode = StatusCodes.Status200OK,
                Result = users.Select(u => new UserResponse
                {
                    Id = u.Id,
                    Name = u.Name,
                    Phone = u.Phone,
                    Email = u.Email,
                    Gender = u.Gender,
                    Status = u.Status,
                    RoleId = u.RoleId,
                    RoleName = u.Role?.Name
                }).ToList()
            };
            return response;
        }

        public async Task<BasicResponse> GetUserByIdAsync(string id)
        {
            var user = _unitOfWork.UserRepository.GetByID(id);
            if (user == null)
            {
                return new BasicResponse
                {
                    IsSuccess = false,
                    Message = "User ID " + id + " does not exist",
                    StatusCode = StatusCodes.Status404NotFound
                };
            }

            var response = new BasicResponse
            {
                IsSuccess = true,
                Message = "Get user by ID successfully",
                StatusCode = StatusCodes.Status200OK,
                Result = new UserResponse
                {
                    Id = user.Id,
                    Name = user.Name,
                    Phone = user.Phone,
                    Email = user.Email,
                    Gender = user.Gender,
                    Status = user.Status,
                    RoleId = user.RoleId,
                    RoleName = user.Role?.Name
                }
            };
            return response;
        }

        public async Task<BasicResponse> CreateUserAsync(CreateUserRequest createUserRequest)
        {
            if (createUserRequest.RoleId.HasValue)
            {
                var role = _unitOfWork.RoleRepository.GetByID(createUserRequest.RoleId.Value);
                if (role == null)
                {
                    return new BasicResponse
                    {
                        IsSuccess = false,
                        Message = "Role ID " + createUserRequest.RoleId + " does not exist",
                        StatusCode = StatusCodes.Status404NotFound
                    };
                }
            }

            var newUser = new User
            {
                Id = Guid.NewGuid().ToString(),
                Name = createUserRequest.Name,
                Phone = createUserRequest.Phone,
                Email = createUserRequest.Email,
                Password = PasswordUtil.HashPassword(createUserRequest.Password),
                Gender = createUserRequest.Gender,
                Status = createUserRequest.Status,
                RoleId = createUserRequest.RoleId
            };

            _unitOfWork.UserRepository.Insert(newUser);
            await _unitOfWork.SaveAsync();

            var response = new BasicResponse
            {
                IsSuccess = true,
                Message = "Create new user successfully",
                StatusCode = StatusCodes.Status201Created,
                Result = new UserResponse
                {
                    Id = newUser.Id,
                    Name = newUser.Name,
                    Phone = newUser.Phone,
                    Email = newUser.Email,
                    Gender = newUser.Gender,
                    Status = newUser.Status,
                    RoleId = newUser.RoleId,
                    RoleName = newUser.Role?.Name
                }
            };
            return response;
        }

        public async Task<BasicResponse> UpdateUserAsync(UpdateUserRequest updateUserRequest)
        {
            var user = _unitOfWork.UserRepository.GetByID(updateUserRequest.Id);
            if (user == null)
            {
                return new BasicResponse
                {
                    IsSuccess = false,
                    Message = "User ID " + updateUserRequest.Id + " does not exist",
                    StatusCode = StatusCodes.Status404NotFound
                };
            }

            if (updateUserRequest.RoleId.HasValue)
            {
                var role = _unitOfWork.RoleRepository.GetByID(updateUserRequest.RoleId.Value);
                if (role == null)
                {
                    return new BasicResponse
                    {
                        IsSuccess = false,
                        Message = "Role ID " + updateUserRequest.RoleId + " does not exist",
                        StatusCode = StatusCodes.Status404NotFound
                    };
                }
            }

            user.Name = updateUserRequest.Name;
            user.Phone = updateUserRequest.Phone;
            user.Email = updateUserRequest.Email;
            user.Password = PasswordUtil.HashPassword(updateUserRequest.Password);
            user.Gender = updateUserRequest.Gender;
            user.Status = updateUserRequest.Status;
            user.RoleId = updateUserRequest.RoleId;

            _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.SaveAsync();

            var response = new BasicResponse
            {
                IsSuccess = true,
                Message = "Update user successfully",
                StatusCode = StatusCodes.Status200OK,
                Result = new UserResponse
                {
                    Id = user.Id,
                    Name = user.Name,
                    Phone = user.Phone,
                    Email = user.Email,
                    Gender = user.Gender,
                    Status = user.Status,
                    RoleId = user.RoleId,
                    RoleName = user.Role?.Name
                }
            };
            return response;
        }

        public async Task<BasicResponse> DeleteUserAsync(string id)
        {
            var user = _unitOfWork.UserRepository.GetByID(id);
            if (user == null)
            {
                return new BasicResponse
                {
                    IsSuccess = false,
                    Message = "User ID " + id + " does not exist",
                    StatusCode = StatusCodes.Status404NotFound
                };
            }

            user.Status = false;
            _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.SaveAsync();

            var response = new BasicResponse
            {
                IsSuccess = true,
                Message = "Delete user successfully",
                StatusCode = StatusCodes.Status200OK
            };
            return response;
        }
    }
}