using NextStar.Framework.EntityFrameworkCore.Input;
using NextStar.Framework.EntityFrameworkCore.Sort;

namespace NextStar.BlogService.Core.Entities;

public class AuthorSelectInput:ICommonInput
{
    public string SearchText { get; set; }
    public List<SortDescriptor> Sorts { get; set; }
    public int PageSize { get; set; }
    public int PageIndex { get; set; }
}