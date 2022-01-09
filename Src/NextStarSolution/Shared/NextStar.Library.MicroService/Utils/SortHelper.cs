using NextStar.Library.MicroService.Consts;

namespace NextStar.Library.MicroService.Utils;

public class SortHelper
{
    public static string GetSortString(List<SortDescriptor>? sorts)
    {
        if (sorts == null || sorts.Count == 0)
            return "";

        List<string> sortStrings = new List<string>();

        foreach (SortDescriptor sort in sorts.Where(s => s.PropertyName != null))
        {
            if (sort.Direction == SortDirection.Desc)
            {
                sortStrings.Add(string.Format("{0} desc", sort.PropertyName));
            }
            else
            {
                sortStrings.Add(sort.PropertyName!);
            }
        }
        return string.Join(',', sortStrings);
    }

    /// <summary>
    /// 判断指定Type是否存在对应的属性
    /// </summary>
    /// <param name="sorts"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public static string GetSortString(List<SortDescriptor>? sorts, Type type)
    {
        if (sorts == null || sorts.Count == 0)
            return "";

        List<string> sortStrings = new List<string>();

        foreach (SortDescriptor sort in sorts.Where(s => s.PropertyName != null))
        {
            if (type.GetProperty(sort.PropertyName!) != null)
            {
                if (sort.Direction == SortDirection.Desc)
                {
                    sortStrings.Add(string.Format("{0} desc", sort.PropertyName));
                }
                else
                {
                    sortStrings.Add(sort.PropertyName!);
                }
            }
        }
        return string.Join(',', sortStrings);
    }
}