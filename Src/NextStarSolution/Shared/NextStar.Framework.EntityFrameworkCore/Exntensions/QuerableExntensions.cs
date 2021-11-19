using System.Linq;
using NextStar.Framework.EntityFrameworkCore.Sort;
using System.Linq.Dynamic.Core;
using NextStar.Framework.EntityFrameworkCore.Filter;
using NextStar.Framework.EntityFrameworkCore.Input;

namespace NextStar.Framework.EntityFrameworkCore.Exntensions;

public static class QuerableExntensions
{
    /// <summary>
    /// 自定义排序和分页
    /// </summary>
    /// <param name="source"></param>
    /// <param name="commonInput"></param>
    /// <param name="defaultSort">默认Id排序</param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IQueryable<T> SortPagination<T>(this IQueryable<T> source, ICommonInput commonInput,
        string defaultSort = "Id")
    {
        var sortString = SortHelper.GetSortString(commonInput.Sorts);
        if (string.IsNullOrWhiteSpace(sortString))
            sortString = defaultSort;
        return source.OrderBy(sortString).Skip((commonInput.PageIndex - 1) * commonInput.PageSize)
            .Take(commonInput.PageSize);
    }

    public static IQueryable<T> Filter<T>(this IQueryable<T> source, IFilter filter)
    {
        if (filter.Filters.Any())
        {
            return source;
        }

        foreach (var filterItem in filter.Filters)
        {
            if (filterItem.Values.Any())
            {
                if (filterItem.Values.Count() == 1)
                {
                    source = source.Where("x => x.{0} == {1}", filterItem.PropertyName.ToUpperFirstLetter(),
                        filterItem.Values[0]);
                }
                else
                {
                    source = source.Where("x => {0}.Contains(x.{1})", filterItem.Values,
                        filterItem.PropertyName.ToUpperFirstLetter());
                }
            }
        }

        return source;
    }
}