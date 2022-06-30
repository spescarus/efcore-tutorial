using System;
using System.Collections.Generic;

namespace WebMvc;

public class PaginatedList<T> : List<T>
{
    public int PageIndex  { get; private set; }
    public int TotalPages { get; private set; }

    public PaginatedList(IReadOnlyCollection<T> items,
                         long                   count,
                         int                    pageIndex,
                         int                    pageSize)
    {
        PageIndex  = pageIndex;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);

        this.AddRange(items);
    }

    public bool HasPreviousPage => PageIndex > 1;

    public bool HasNextPage => PageIndex < TotalPages;

    public static PaginatedList<T> CreateAsync(IReadOnlyCollection<T> items,
                                               long                   totalRecords,
                                               int                    pageIndex,
                                               int                    pageSize)
    {
        // var count = await source.CountAsync();
        // var items = await source.Skip((offset - 1) * limit)
        //                         .Take(limit)
        //                         .ToListAsync();
        return new PaginatedList<T>(items, totalRecords, pageIndex, pageSize);
    }
}