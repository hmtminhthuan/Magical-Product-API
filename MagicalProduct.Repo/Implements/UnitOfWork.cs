using MagicalProduct.API.Models;
using MagicalProduct.Repo.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace MagicalProduct.Repo.Implement;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly MagicalProductContext context;
    private IGenericRepository<User> userRepository;
    private IGenericRepository<Product> productRepository;
    private IGenericRepository<Category> categoryRepository;

    public UnitOfWork(MagicalProductContext context)
    {
        this.context = context;
    }

    public IGenericRepository<User> UserRepository
    {
        get
        {
            return userRepository ??= new GenericRepository<User>(context);
        }
    }
    public IGenericRepository<Product> ProductRepository
    {
        get
        {
            return productRepository ??= new GenericRepository<Product>(context);
        }
    }
    public IGenericRepository<Category> CategoryRepository
    {
        get
        {
            return categoryRepository ??= new GenericRepository<Category>(context);
        }
    }

    public void Save()
    {
        var validationErrors = context.ChangeTracker.Entries<IValidatableObject>()
            .SelectMany(e => e.Entity.Validate(null))
            .Where(e => e != ValidationResult.Success)
            .ToArray();
        if (validationErrors.Any())
        {
            var exceptionMessage = string.Join(Environment.NewLine,
                validationErrors.Select(error => $"Properties {error.MemberNames} Error: {error.ErrorMessage}"));
            throw new Exception(exceptionMessage);
        }
        context.SaveChanges();
    }

    async public Task SaveAsync()
    {
        var validationErrors = context.ChangeTracker.Entries<IValidatableObject>()
            .SelectMany(e => e.Entity.Validate(null))
            .Where(e => e != ValidationResult.Success)
            .ToArray();
        if (validationErrors.Any())
        {
            var exceptionMessage = string.Join(Environment.NewLine,
                validationErrors.Select(error => $"Properties {error.MemberNames} Error: {error.ErrorMessage}"));
            throw new Exception(exceptionMessage);
        }
        await context.SaveChangesAsync();
    }

    private bool disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                context.Dispose();
            }
            disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
