using System;
using System.Collections.Generic;

namespace WebApp;

public class PaginatedList<T> : List<T>
{
    public int PageIndex  { get; }
    public int TotalPages { get; }

    public PaginatedList(IReadOnlyCollection<T> items,
                         long                   count,
                         int                    pageIndex,
                         int                    pageSize)
    {
        PageIndex  = pageIndex;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);

        AddRange(items);
    }

    public bool HasPreviousPage => PageIndex > 1;

    public bool HasNextPage => PageIndex < TotalPages;

    public static PaginatedList<T> CreateAsync(IReadOnlyCollection<T> items,
                                               long                   totalRecords,
                                               int                    pageIndex,
                                               int                    pageSize)
    {
        return new PaginatedList<T>(items, totalRecords, pageIndex, pageSize);
    }
}