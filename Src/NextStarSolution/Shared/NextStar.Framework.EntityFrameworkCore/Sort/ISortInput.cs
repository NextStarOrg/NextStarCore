using System.Collections.Generic;

namespace NextStar.Framework.EntityFrameworkCore.Sort;

public interface ISortInput
{ 
    public List<SortDescriptor> Sorts { get; set; }
}