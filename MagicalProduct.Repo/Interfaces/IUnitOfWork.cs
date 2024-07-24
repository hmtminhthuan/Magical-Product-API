using MagicalProduct.API.Models;

namespace MagicalProduct.Repo.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<User> UserRepository { get; }
        IGenericRepository<Order> OrderRepository { get; }
        IGenericRepository<OrderDetail> OrderDetailRepository { get; }
        IGenericRepository<PaymentMethod> PaymentMethodRepository { get; }

        void Save();
        Task SaveAsync();

    }
}
