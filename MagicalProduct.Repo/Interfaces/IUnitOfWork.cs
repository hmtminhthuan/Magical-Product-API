using MagicalProduct.API.Models;

namespace MagicalProduct.Repo.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<User> UserRepository { get; }
        void Save();
        Task SaveAsync();

    }
}
