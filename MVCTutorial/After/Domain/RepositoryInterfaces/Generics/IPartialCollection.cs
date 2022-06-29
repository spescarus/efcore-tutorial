namespace Domain.RepositoryInterfaces.Generics;

public interface IPartialCollection<TEntity>
{
    public IReadOnlyCollection<TEntity> Values { get; set; }
    public long Count { get; set; }
    public int? Offset { get; set; }
    public int? Limit { get; set; }
}