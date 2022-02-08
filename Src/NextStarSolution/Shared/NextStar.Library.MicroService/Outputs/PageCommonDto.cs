namespace NextStar.Library.MicroService.Outputs;

public class PageCommonDto<T>
{
    public List<T> Data { get; set; } = new List<T>();
    public int TotalCount { get; set; } = 0;
}