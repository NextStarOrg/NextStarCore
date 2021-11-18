namespace NextStar.Framework.EntityFrameworkCore.Sort;

public class SortDescriptor
{
    /// <summary>
    ///  排序方向 0:升序  1：降序
    /// </summary>
    public SortDirection Direction { get; set; }

    /// <summary>
    ///  属性名
    /// </summary>
    public string PropertyName { get; set; } = null!;
}