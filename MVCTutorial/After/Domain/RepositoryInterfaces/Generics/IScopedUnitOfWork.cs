namespace Domain.RepositoryInterfaces.Generics;

public interface IScopedUnitOfWork
{
    Task SaveAsync(CancellationToken   cancellationToken = default); 
    Task CommitAsync(CancellationToken cancellationToken = default);
}