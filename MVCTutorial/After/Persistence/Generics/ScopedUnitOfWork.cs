using Domain.RepositoryInterfaces.Generics;
using Microsoft.EntityFrameworkCore.Storage;
using Persistence.Context;

namespace Persistence.Generics;

public class ScopedUnitOfWork: IScopedUnitOfWork
{
    private readonly EfCoreContext      _context;
    private readonly IDbContextTransaction _transaction;
    public ScopedUnitOfWork(EfCoreContext context, IDbContextTransaction transaction)
    {
        _context     = context;
        _transaction = transaction;
    }

    public Task SaveAsync(CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }

    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            await _transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await _transaction.RollbackAsync(cancellationToken);
        }
        finally{
            _transaction?.Dispose();
        }
    }
}