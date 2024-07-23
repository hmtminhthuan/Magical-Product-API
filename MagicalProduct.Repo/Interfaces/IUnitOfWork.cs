using MagicalProduct.API.Models;

namespace MagicalProduct.Repo.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<User> UserRepository { get; }

        IGenericRepository<PaymentMethod> PaymentMethodRepository { get; }

        IGenericRepository<Card> CardRepository { get; }
        void Save();
        Task SaveAsync();

    }
}
