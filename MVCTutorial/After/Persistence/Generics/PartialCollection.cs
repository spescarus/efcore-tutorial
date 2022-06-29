using Domain.RepositoryInterfaces.Generics;

namespace Persistence.Generics;

public class PartialCollection<TEntity> : IPartialCollection<TEntity>
{
    public PartialCollection(IReadOnlyCollection<TEntity> values, long count, int offset, int limit)
    {
        Values = values;
        Count = count;
        Offset = offset;
        Limit = limit;
    }

    public PartialCollection(IReadOnlyCollection<TEntity> values, long count)
    {
        Values = values;
        Count = count;
    }

    public IReadOnlyCollection<TEntity> Values { get; set; }
    public long Count { get; set; }
    public int? Offset { get; set; }
    public int? Limit { get; set; }
}