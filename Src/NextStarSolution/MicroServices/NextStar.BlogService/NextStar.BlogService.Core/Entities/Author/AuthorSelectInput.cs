using NextStar.Framework.EntityFrameworkCore.Input;
using NextStar.Framework.EntityFrameworkCore.Sort;

namespace NextStar.BlogService.Core.Entities;

public class AuthorSelectInput:ICommonInput
{
    public string SearchText { get; set; }
    public List<SortDescriptor> Sorts { get; set; } = new List<SortDescriptor>();
    public int PageSize { get; set; } = 10;
    public int PageIndex { get; set; } = 1;
}