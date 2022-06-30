
namespace ApplicationServices.Services;

public sealed class PartialCollectionResponse<TEntity>
{
    /// <summary>
    ///     The entities with pagination
    /// </summary>
    public IReadOnlyCollection<TEntity> Values { get; set; }

    /// <summary>
    ///     The current offset used for query the values
    /// </summary>
    public int? Offset { get; set; }

    /// <summary>
    ///     The current limit used for query the values
    /// </summary>
    public int? Limit { get; set; }

    /// <summary>
    ///     The total of entities with search applied, without pagination
    /// </summary>
    public long RecordsFiltered { get; set; }

    /// <summary>
    ///     The total of entities without pagination and without search applied
    /// </summary>
    public long RecordsTotal { get; set; }

}