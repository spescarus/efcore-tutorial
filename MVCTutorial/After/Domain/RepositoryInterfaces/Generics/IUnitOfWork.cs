namespace Domain.RepositoryInterfaces.Generics;

public interface IUnitOfWork
{
    Task<IScopedUnitOfWork> CreateScopeAsync();
}