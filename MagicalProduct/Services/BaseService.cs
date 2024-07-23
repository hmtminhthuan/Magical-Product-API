using MagicalProduct.Repo.Interfaces;
using System.Security.Claims;
using AutoMapper;

namespace MagicalProduct.API.Services
{
    public abstract class BaseService<T> where T : class
    {
        protected IUnitOfWork _unitOfWork;
        protected ILogger<T> _logger;
        protected IMapper _mapper;
        protected IHttpContextAccessor _httpContextAccessor;

        public BaseService(IUnitOfWork unitOfWork, ILogger<T> logger,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        protected string GetRoleFromJwt()
        {
            string role = _httpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.Role);
            return role;
        }
    }
}