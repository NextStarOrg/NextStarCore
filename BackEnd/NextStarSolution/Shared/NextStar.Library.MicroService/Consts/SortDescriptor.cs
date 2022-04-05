namespace NextStar.Library.MicroService.Consts;

public class SortDescriptor
{
    /// <summary>
    ///  排序方向 0:升序  1：降序
    /// </summary>
    public SortDirection Direction { get; set; }

    /// <summary>
    ///  属性名
    /// </summary>
    public string PropertyName { get; set; }

    /// <summary>
    /// 多字段排序顺序
    /// </summary>
    public int Multiple { get; set; } = 0;
}