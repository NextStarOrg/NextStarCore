namespace NextStar.Framework.EntityFrameworkCore.Filter;

public interface IFilterInput
{
    public List<FilterDescriptor> Filters { get; set; }
}