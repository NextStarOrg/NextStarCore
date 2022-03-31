using NextStar.Library.MicroService.Consts;

namespace NextStar.Library.MicroService.Inputs;

public class PageSortInput : PaginationInput
{
    public List<SortDescriptor> Sorts { get; set; } = new List<SortDescriptor>();
}