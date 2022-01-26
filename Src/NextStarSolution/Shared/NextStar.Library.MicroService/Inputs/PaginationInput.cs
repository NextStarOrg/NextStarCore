namespace NextStar.Library.MicroService.Inputs;

public class PaginationInput
{
    /// <summary>
    /// 分页大小
    /// </summary>
    public int PageSize { get; set; } = 10;
    /// <summary>
    /// 页码数
    /// </summary>
    public int PageNumber { get; set; } = 1;
}