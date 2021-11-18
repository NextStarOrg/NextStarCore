using NextStar.Framework.EntityFrameworkCore.Pagination;
using NextStar.Framework.EntityFrameworkCore.Sort;

namespace NextStar.Framework.EntityFrameworkCore.Input;

public interface ICommonInput : ISortInput, IPaginationInput
{
}