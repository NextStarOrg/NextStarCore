namespace NextStar.Framework.EntityFrameworkCore.Pagination;

public interface IPaginationInput
{
    public int PageSize { get; set; }
    public int PageIndex { get; set; }
}