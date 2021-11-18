using System.Collections.Generic;

namespace NextStar.Framework.EntityFrameworkCore.Sort;

public static class SortHelper
{
    public static string GetSortString(List<SortDescriptor> sorts)
    {
        if (sorts == null || sorts.Count == 0)
            return "";

        List< string> sortStrings = new List<string>();

        foreach (SortDescriptor sort in sorts)
        {
            if(sort.Direction == SortDirection.Desc)
            {
                sortStrings.Add(string.Format("{0} desc", sort.PropertyName));
            }
            else
            {
                sortStrings.Add(sort.PropertyName);
            }
        }
        return string.Join(',', sortStrings);
    }

}