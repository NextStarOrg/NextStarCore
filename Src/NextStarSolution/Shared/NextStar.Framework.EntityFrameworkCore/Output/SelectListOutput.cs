namespace NextStar.Framework.EntityFrameworkCore.Output;

public class SelectListOutput<T>
{
    public int Count { get; set; }
    public List<T> Data { get; set; }
}