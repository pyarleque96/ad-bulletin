using AdBulletin.Infrastructure.Transport;
using Microsoft.EntityFrameworkCore;

namespace AdBulletin.Domain.Extensions;

public static class PaginationExtensions
{
    public static async Task<BaseResult<IEnumerable<T>>> PaginateAsync<T>(
        this IQueryable<T> source, BasePaginationParameter paginationParameter)
    {
        var totalCount = await source.CountAsync();

        var items = await source
            .Skip((paginationParameter.PageIndex - 1) * paginationParameter.PageSize)
            .Take(paginationParameter.PageSize)
            .ToListAsync();

        var result = new BaseResult<IEnumerable<T>>
        {
            Pagination = new PaginationRawResult
            {
                PageIndex = paginationParameter.PageIndex,
                PageSize = paginationParameter.PageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)paginationParameter.PageSize),
                HasPreviousPage = paginationParameter.PageIndex > 1,
                HasNextPage = paginationParameter.PageIndex < (int)Math.Ceiling(totalCount / (double)paginationParameter.PageSize),
                IndexFrom = (paginationParameter.PageIndex - 1) * paginationParameter.PageSize + 1
            },
            Result = items
        };

        return result;
    }
}
