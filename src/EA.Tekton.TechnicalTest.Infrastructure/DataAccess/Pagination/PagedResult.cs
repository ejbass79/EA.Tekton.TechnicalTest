using Microsoft.EntityFrameworkCore;

namespace EA.Tekton.TechnicalTest.Infrastructure.DataAccess.Pagination;

public class PagedResult<T> : List<T>
{
    public int CurrentPage { get; }

    public int TotalPages { get; }

    public int PageSize { get; }

    public int TotalCount { get; }

    public PagedResult(IEnumerable<T> items, int count, int pageNumber, int pageSize)
    {
        TotalCount = count;
        PageSize = pageSize;
        CurrentPage = pageNumber;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        AddRange(items);
    }

    public static async Task<PagedResult<T>> ToPagedListAsync(IQueryable<T> source, int pageNumber, int pageSize, string orderBy, bool ascending = true)
    {
        var count = source.Count();
        var items = await source.OrderByPropertyOrField(orderBy, ascending).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        return new PagedResult<T>(items, count, pageNumber, pageSize);
    }
}
