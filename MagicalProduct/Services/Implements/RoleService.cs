using MagicalProduct.Repo.Interfaces;
using MagicalProduct.API.Payload.Request;
using MagicalProduct.API.Payload.Response;
using MagicalProduct.API.Models;
using MagicalProduct.API.Services.Interfaces;
using AutoMapper;

namespace MagicalProduct.API.Services.Implements
{
    public class RoleService : BaseService<RoleService>, IRoleService
    {
        public RoleService(IUnitOfWork unitOfWork, ILogger<RoleService> logger, IMapper mapper,
            IHttpContextAccessor httpContextAccessor) : base(unitOfWork, logger, mapper, httpContextAccessor)
        {
        }
        public async Task<BasicResponse> GetAllRolesAsync()
        {
            var roles = _unitOfWork.RoleRepository.Get();
            var response = new BasicResponse
            {
                IsSuccess = true,
                Message = "Get all roles successfully",
                StatusCode = StatusCodes.Status200OK,
                Result = roles.Select(r => new RoleResponse
                {
                    Id = r.Id,
                    Name = r.Name
                }).ToList()
            };
            return response;
        }

        public async Task<BasicResponse> GetRoleByIdAsync(int id)
        {
            var role = _unitOfWork.RoleRepository.GetByID(id);
            if (role == null)
            {
                return new BasicResponse
                {
                    IsSuccess = false,
                    Message = "Role ID " + id + " does not exist",
                    StatusCode = StatusCodes.Status404NotFound
                };
            }

            var response = new BasicResponse
            {
                IsSuccess = true,
                Message = "Get role by ID successfully",
                StatusCode = StatusCodes.Status200OK,
                Result = new RoleResponse
                {
                    Id = role.Id,
                    Name = role.Name
                }
            };
            return response;
        }

        public async Task<BasicResponse> CreateRoleAsync(CreateRoleRequest createRoleRequest)
        {
            var newRole = new Role
            {
                Name = createRoleRequest.Name
            };

            _unitOfWork.RoleRepository.Insert(newRole);
            await _unitOfWork.SaveAsync();

            var response = new BasicResponse
            {
                IsSuccess = true,
                Message = "Create new role successfully",
                StatusCode = StatusCodes.Status201Created,
                Result = new RoleResponse
                {
                    Id = newRole.Id,
                    Name = newRole.Name
                }
            };
            return response;
        }

        public async Task<BasicResponse> UpdateRoleAsync(UpdateRoleRequest updateRoleRequest)
        {
            var role = _unitOfWork.RoleRepository.GetByID(updateRoleRequest.Id);
            if (role == null)
            {
                return new BasicResponse
                {
                    IsSuccess = false,
                    Message = "Role ID " + updateRoleRequest.Id + " does not exist",
                    StatusCode = StatusCodes.Status404NotFound
                };
            }

            role.Name = updateRoleRequest.Name;

            _unitOfWork.RoleRepository.Update(role);
            await _unitOfWork.SaveAsync();

            var response = new BasicResponse
            {
                IsSuccess = true,
                Message = "Update role successfully",
                StatusCode = StatusCodes.Status200OK,
                Result = new RoleResponse
                {
                    Id = role.Id,
                    Name = role.Name
                }
            };
            return response;
        }

        public async Task<BasicResponse> DeleteRoleAsync(int id)
        {
            var role = _unitOfWork.RoleRepository.GetByID(id);
            if (role == null)
            {
                return new BasicResponse
                {
                    IsSuccess = false,
                    Message = "Role ID " + id + " does not exist",
                    StatusCode = StatusCodes.Status404NotFound
                };
            }

            _unitOfWork.RoleRepository.Delete(id);
            await _unitOfWork.SaveAsync();

            var response = new BasicResponse
            {
                IsSuccess = true,
                Message = "Delete role successfully",
                StatusCode = StatusCodes.Status200OK
            };
            return response;
        }
    }
}
