using MagicalProduct.Repo.Interfaces;
using System.Security.Claims;
using AutoMapper;

namespace MagicalProduct.API.Services
{
    public abstract class BaseService<T> where T : class
    {
        protected IUnitOfWork _unitOfWork;
        protected ILogger<T> _logger;

        public BaseService(IUnitOfWork unitOfWork, ILogger<T> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
    }
}