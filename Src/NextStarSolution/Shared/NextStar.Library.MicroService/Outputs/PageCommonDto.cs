namespace NextStar.Library.MicroService.Outputs;

public class PageCommonDto<T>
{
    public T Data { get; set; } = default;
    public int TotalCount { get; set; } = 0;
}