using NextStar.Library.MicroService.Inputs;
using System.Linq.Dynamic.Core;

namespace NextStar.Library.MicroService.Utils;

public static class QueryableHelper
{
    /// <summary>
    /// Queryable 统一分页
    /// </summary>
    /// <param name="query"></param>
    /// <param name="paginationInput"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IQueryable<T> CommonPagination<T>(this IQueryable<T> query, PaginationInput paginationInput)
    {
        return query.Skip((paginationInput.PageNumber - 1) * paginationInput.PageSize)
            .Take(paginationInput.PageSize);
    }
    
    /// <summary>
    /// Queryable 统一分页和排序
    /// </summary>
    /// <param name="query"></param>
    /// <param name="pageSortInput"></param>
    /// <param name="defaultSort">"Id desc","Id","Id desc,CreatedTime asc"</param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IQueryable<T> CommonPageSort<T>(this IQueryable<T> query, PageSortInput pageSortInput,string defaultSort)
    {
        string sortString = SortHelper.GetSortString(pageSortInput.Sorts);
        if (string.IsNullOrWhiteSpace(sortString))
            sortString = defaultSort;
        return query.OrderBy(sortString).CommonPagination(pageSortInput);
    }
}