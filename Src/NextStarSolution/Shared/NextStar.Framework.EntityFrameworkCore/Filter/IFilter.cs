namespace NextStar.Framework.EntityFrameworkCore.Filter;

public interface IFilter
{
    public List<FilterDescriptor> Filters { get; set; }
}