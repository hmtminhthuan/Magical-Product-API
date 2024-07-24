using MagicalProduct.API.Models;

namespace MagicalProduct.Repo.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<User> UserRepository { get; }
        IGenericRepository<Product> ProductRepository { get; }
        IGenericRepository<Category> CategoryRepository { get; }
        void Save();
        Task SaveAsync();

    }
}
