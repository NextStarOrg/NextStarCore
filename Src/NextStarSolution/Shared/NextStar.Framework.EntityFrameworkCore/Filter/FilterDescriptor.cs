namespace NextStar.Framework.EntityFrameworkCore.Filter;

public class FilterDescriptor
{
    /// <summary>
    ///  属性名
    /// </summary>
    public string PropertyName { get; set; } = null!;

    public List<string> Values { get; set; } = null!;
}